﻿namespace System.Data.Entity.Internal
{
    using System.Collections.Generic;
    using System.Data.Metadata.Edm;
    using System.Data.Objects;

    /// <summary>
    ///     This is version of an internal interface that already exists in System.Data.Entity that
    ///     is implemented by <see cref = "System.Data.Objects.ObjectStateEntry" />.  Using this interface allows state
    ///     entries to be mocked for unit testing.  The plan is to remove this version of the
    ///     interface and use the one in System.Data.Entity once we roll into the framework.
    ///     Note that some members may need to be added to the interface in the framework when
    ///     we combine the two.
    /// </summary>
    internal interface IEntityStateEntry
    {
        object Entity { get; }
        EntityState State { get; }
        void ChangeState(EntityState state);
        DbUpdatableDataRecord CurrentValues { get; }
        DbUpdatableDataRecord GetUpdatableOriginalValues();
        EntitySetBase EntitySet { get; }
        EntityKey EntityKey { get; }
        IEnumerable<string> GetModifiedProperties();
        void SetModifiedProperty(string propertyName);
        
        bool IsPropertyChanged(string propertyName);
        void RejectPropertyChanges(string propertyName);

    }
}