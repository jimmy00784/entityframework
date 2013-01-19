// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.
namespace System.Data.Entity.Config
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Internal;
    using System.Data.Entity.Resources;
    using System.Data.Entity.Utilities;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// This class is responsible for managing the app-domain instance of the <see cref="DbConfiguration"/> class.
    /// This includes loading from config, discovery from the context assembly and pushing/popping configurations
    /// used by <see cref="DbContextInfo"/>.
    /// </summary>
    internal class DbConfigurationManager
    {
        private static readonly DbConfigurationManager _configManager
            = new DbConfigurationManager(new DbConfigurationLoader(), new DbConfigurationFinder());

        private readonly DbConfigurationLoader _loader;
        private readonly DbConfigurationFinder _finder;
        
        private readonly Lazy<DbConfiguration> _configuration;
        private DbConfiguration _newConfiguration = new DbConfiguration();
        
        private readonly object _lock = new object();

        // We don't need a dictionary here, just a set, but there is no ConcurrentSet in the BCL.
        private readonly ConcurrentDictionary<Assembly, object> _knownAssemblies = new ConcurrentDictionary<Assembly, object>();
        private readonly Lazy<List<Tuple<AppConfig, DbConfiguration>>> _configurationOverrides
            = new Lazy<List<Tuple<AppConfig, DbConfiguration>>>(
                () => new List<Tuple<AppConfig, DbConfiguration>>());

        public DbConfigurationManager(DbConfigurationLoader loader, DbConfigurationFinder finder)
        {
            //Contract.Requires(loader != null);
            //Contract.Requires(finder != null);

            _loader = loader;
            _finder = finder;
            _configuration = new Lazy<DbConfiguration>(
                () =>
                    {
                        _newConfiguration.Lock();
                        return _newConfiguration;
                    });
        }

        public static DbConfigurationManager Instance
        {
            get { return _configManager; }
        }

        public virtual DbConfiguration GetConfiguration()
        {
            // The common case is that no overrides have ever been set so we don't take the time to do
            // the locking and checking.
            if (_configurationOverrides.IsValueCreated)
            {
                lock (_lock)
                {
                    if (_configurationOverrides.Value.Count != 0)
                    {
                        return _configurationOverrides.Value.Last().Item2;
                    }
                }
            }

            return _configuration.Value;
        }

        public virtual void SetConfiguration(DbConfiguration configuration)
        {
            //Contract.Requires(configuration != null);

            configuration = _loader.TryLoadFromConfig(AppConfig.DefaultInstance) ?? configuration;
            _newConfiguration = configuration;

            if (_configuration.Value.GetType() != configuration.GetType())
            {
                if (_configuration.Value.GetType() == typeof(DbConfiguration))
                {
                    throw new InvalidOperationException(Strings.DefaultConfigurationUsedBeforeSet(configuration.GetType().Name));
                }

                throw new InvalidOperationException(
                    Strings.ConfigurationSetTwice(configuration.GetType().Name, _configuration.Value.GetType().Name));
            }
        }

        public virtual void EnsureLoadedForContext(Type contextType)
        {
            //Contract.Requires(contextType != null);
            //Contract.Requires(typeof(DbContext).IsAssignableFrom(contextType));

            var contextAssembly = contextType.Assembly;

            if (contextType == typeof(DbContext)
                || _knownAssemblies.ContainsKey(contextAssembly))
            {
                return;
            }

            if (_configurationOverrides.IsValueCreated)
            {
                lock (_lock)
                {
                    if (_configurationOverrides.Value.Count != 0)
                    {
                        return;
                    }
                }
            }

            if (!_configuration.IsValueCreated)
            {
                var foundConfiguration = _finder.TryCreateConfiguration(contextType.Assembly.GetAccessibleTypes());
                if (foundConfiguration != null)
                {
                    SetConfiguration(foundConfiguration);
                }
            }
            else if (!contextAssembly.IsDynamic) // Don't throw for proxy contexts created in dynamic assemblies
            {
                var foundType = _finder.TryFindConfigurationType(contextType.Assembly.GetAccessibleTypes());
                if (!typeof(DbNullConfiguration).IsAssignableFrom(foundType))
                {
                    if (_configuration.Value.GetType() == typeof(DbConfiguration))
                    {
                        if (foundType != null)
                        {
                            throw new InvalidOperationException(Strings.ConfigurationNotDiscovered(foundType.Name));
                        }
                    }
                    else
                    {
                        if (foundType == null
                            || foundType != _configuration.Value.GetType())
                        {
                            throw new InvalidOperationException(
                                Strings.SetConfigurationNotDiscovered(_configuration.Value.GetType().Name, contextType.Name));
                        }
                    }
                }
            }

            _knownAssemblies.TryAdd(contextAssembly, null);
        }

        public virtual void PushConfiguration(AppConfig config, Type contextType)
        {
            //Contract.Requires(config != null);
            //Contract.Requires(contextType != null);
            //Contract.Requires(typeof(DbContext).IsAssignableFrom(contextType));

            var configuration = _loader.TryLoadFromConfig(config)
                                ?? _finder.TryCreateConfiguration(contextType.Assembly.GetAccessibleTypes())
                                ?? new DbConfiguration();

            configuration.AddAppConfigResolver(new AppConfigDependencyResolver(config));

            configuration.Lock();

            lock (_lock)
            {
                _configurationOverrides.Value.Add(Tuple.Create(config, configuration));
            }
        }

        public virtual void PopConfiguration(AppConfig config)
        {
            //Contract.Requires(config != null);

            lock (_lock)
            {
                var configuration = _configurationOverrides.Value.FirstOrDefault(c => c.Item1 == config);
                if (configuration != null)
                {
                    _configurationOverrides.Value.Remove(configuration);
                }
            }
        }
    }
}
