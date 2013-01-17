// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.
namespace System.Data.Entity.Migrations.Model
{
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Data.Metadata.Edm;
    using System.Data.Entity.Spatial;
    using System.Diagnostics.CodeAnalysis;
    using System.Diagnostics.Contracts;
    using System.Globalization;

    /// <summary>
    ///     Represents information about a column.
    /// </summary>
    public class ColumnModel
    {
        private readonly PrimitiveTypeKind _type;
        private readonly Type _clrType;
        private readonly object _clrDefaultValue;

        private TypeUsage _typeUsage;

        /// <summary>
        ///     Initializes a new instance of the  class.
        /// </summary>
        /// <param name = "type">The data type for this column.</param>
        public ColumnModel(PrimitiveTypeKind type)
            : this(type, null)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the  class.
        /// </summary>
        /// <param name = "type">The data type for this column.</param>
        /// <param name = "typeUsage">
        ///     Additional details about the data type.
        ///     This includes details such as maximum length, nullability etc.
        /// </param>
        public ColumnModel(PrimitiveTypeKind type, TypeUsage typeUsage)
        {
            _type = type;
            _typeUsage = typeUsage;
            _clrType = PrimitiveType.GetEdmPrimitiveType(_type).ClrEquivalentType;
            _clrDefaultValue = CreateDefaultValue();
        }

        private object CreateDefaultValue()
        {
            if (_clrType.IsValueType)
            {
                return Activator.CreateInstance(_clrType);
            }

            if (_clrType == typeof(string))
            {
                return string.Empty;
            }

            if (_clrType == typeof(DbGeography))
            {
                return DbGeography.FromText("POINT(0 0)");
            }

            if (_clrType == typeof(DbGeometry))
            {
                return DbGeometry.FromText("POINT(0 0)");
            }
            return new byte[0];
        }

        /// <summary>
        ///     Gets the data type for this column.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods")]
        public virtual PrimitiveTypeKind Type
        {
            get { return _type; }
        }

        /// <summary>
        ///     Gets the CLR type corresponding to the database type of this column.
        /// </summary>
        public virtual Type ClrType
        {
            get { return _clrType; }
        }

        /// <summary>
        ///     Gets the default value for the CLR type corresponding to the database type of this column.
        /// </summary>
        public virtual object ClrDefaultValue
        {
            get { return _clrDefaultValue; }
        }

        /// <summary>
        ///     Gets additional details about the data type of this column.
        ///     This includes details such as maximum length, nullability etc.
        /// </summary>
        public TypeUsage TypeUsage
        {
            get { return _typeUsage ?? (_typeUsage = BuildTypeUsage()); }
        }

        /// <summary>
        ///     Gets or sets the name of the column.
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        ///     Gets or sets a provider specific data type to use for this column.
        /// </summary>
        public virtual string StoreType { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating if this column can store null values.
        /// </summary>
        public virtual bool? IsNullable { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating if values for this column will be generated by the database using the identity pattern.
        /// </summary>
        public virtual bool IsIdentity { get; set; }

        /// <summary>
        ///     Gets or sets the maximum length for this column.
        ///     Only valid for array data types.
        /// </summary>
        public virtual int? MaxLength { get; set; }

        /// <summary>
        ///     Gets or sets the precision for this column.
        ///     Only valid for decimal data types.
        /// </summary>
        public virtual byte? Precision { get; set; }

        /// <summary>
        ///     Gets or sets the scale for this column.
        ///     Only valid for decimal data types.
        /// </summary>
        public virtual byte? Scale { get; set; }

        /// <summary>
        ///     Gets or sets a constant value to use as the default value for this column.
        /// </summary>
        public virtual object DefaultValue { get; set; }

        /// <summary>
        ///     Gets or sets a SQL expression used as the default value for this column.
        /// </summary>
        public virtual string DefaultValueSql { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating if this column is fixed length.
        ///     Only valid for array data types.
        /// </summary>
        public virtual bool? IsFixedLength { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating if this column supports Unicode characters.
        ///     Only valid for textual data types.
        /// </summary>
        public virtual bool? IsUnicode { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating if this column should be configured as a timestamp.
        /// </summary>
        public virtual bool IsTimestamp { get; set; }

        private static readonly Dictionary<PrimitiveTypeKind, int> _typeSize // in bytes
            = new Dictionary<PrimitiveTypeKind, int>
                {
                    { PrimitiveTypeKind.Binary, int.MaxValue },
                    { PrimitiveTypeKind.Boolean, 1 },
                    { PrimitiveTypeKind.Byte, 1 },
                    { PrimitiveTypeKind.DateTime, 8 },
                    { PrimitiveTypeKind.DateTimeOffset, 10 },
                    { PrimitiveTypeKind.Decimal, 17 },
                    { PrimitiveTypeKind.Double, 53 },
                    { PrimitiveTypeKind.Guid, 16 },
                    { PrimitiveTypeKind.Int16, 2 },
                    { PrimitiveTypeKind.Int32, 4 },
                    { PrimitiveTypeKind.Int64, 8 },
                    { PrimitiveTypeKind.SByte, 1 },
                    { PrimitiveTypeKind.Single, 4 },
                    { PrimitiveTypeKind.String, int.MaxValue },
                    { PrimitiveTypeKind.Time, 5 },
                };

        /// <summary>
        ///     Determines if this column is a narrower data type than another column.
        ///     Used to determine if altering the supplied column definition to this definition will result in data loss.
        /// </summary>
        /// <param name = "column">The column to compare to.</param>
        /// <param name = "providerManifest">Details of the database provider being used.</param>
        /// <returns>True if this column is of a narrower data type.</returns>
        public bool IsNarrowerThan(ColumnModel column, DbProviderManifest providerManifest)
        {
            Contract.Requires(column != null);
            Contract.Requires(providerManifest != null);

            var typeUsage = providerManifest.GetStoreType(TypeUsage);
            var otherTypeUsage = providerManifest.GetStoreType(column.TypeUsage);

            return (_typeSize[Type] < _typeSize[column.Type])
                   || !(IsUnicode ?? true) && (column.IsUnicode ?? true)
                   || !(IsNullable ?? true) && (column.IsNullable ?? true)
                   || IsNarrowerThan(typeUsage, otherTypeUsage);
        }

        private static bool IsNarrowerThan(TypeUsage typeUsage, TypeUsage other)
        {
            Contract.Requires(typeUsage != null);
            Contract.Requires(other != null);

            foreach (var facetName in
                new[]
                    {
                        DbProviderManifest.MaxLengthFacetName,
                        DbProviderManifest.PrecisionFacetName,
                        DbProviderManifest.ScaleFacetName
                    })
            {
                Facet facet, otherFacet;
                if (!typeUsage.Facets.TryGetValue(facetName, true, out facet)
                    || !other.Facets.TryGetValue(facet.Name, true, out otherFacet)
                    || (facet.Value == otherFacet.Value))
                {
                    continue;
                }

                var valueAsInt = Convert.ToInt32(facet.Value, CultureInfo.InvariantCulture);
                var otherValueAsInt = Convert.ToInt32(otherFacet.Value, CultureInfo.InvariantCulture);

                if (valueAsInt < otherValueAsInt)
                {
                    return true;
                }
            }

            return false;
        }

        private TypeUsage BuildTypeUsage()
        {
            var primitiveType = PrimitiveType.GetEdmPrimitiveType(Type);

            if (Type == PrimitiveTypeKind.Binary)
            {
                if (MaxLength != null)
                {
                    return TypeUsage.CreateBinaryTypeUsage(
                        primitiveType,
                        IsFixedLength ?? false,
                        MaxLength.Value);
                }

                return TypeUsage.CreateBinaryTypeUsage(
                    primitiveType,
                    IsFixedLength ?? false);
            }

            if (Type == PrimitiveTypeKind.String)
            {
                if (MaxLength != null)
                {
                    return TypeUsage.CreateStringTypeUsage(
                        primitiveType,
                        IsUnicode ?? true,
                        IsFixedLength ?? false,
                        MaxLength.Value);
                }

                return TypeUsage.CreateStringTypeUsage(
                    primitiveType,
                    IsUnicode ?? true,
                    IsFixedLength ?? false);
            }

            if (Type == PrimitiveTypeKind.DateTime)
            {
                return TypeUsage.CreateDateTimeTypeUsage(primitiveType, Precision);
            }

            if (Type == PrimitiveTypeKind.DateTimeOffset)
            {
                return TypeUsage.CreateDateTimeOffsetTypeUsage(primitiveType, Precision);
            }

            if (Type == PrimitiveTypeKind.Decimal)
            {
                if ((Precision != null)
                    || (Scale != null))
                {
                    return TypeUsage.CreateDecimalTypeUsage(
                        primitiveType,
                        Precision ?? 18,
                        Scale ?? 0);
                }

                return TypeUsage.CreateDecimalTypeUsage(primitiveType);
            }

            return (Type == PrimitiveTypeKind.Time)
                       ? TypeUsage.CreateTimeTypeUsage(primitiveType, Precision)
                       : TypeUsage.CreateDefaultTypeUsage(primitiveType);
        }
    }
}
