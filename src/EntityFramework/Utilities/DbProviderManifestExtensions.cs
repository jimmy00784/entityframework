// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.
namespace System.Data.Entity.Utilities
{
    using System.Data.Common;
    using System.Data.Metadata.Edm;
    using System.Diagnostics.Contracts;

    internal static class DbProviderManifestExtensions
    {
        public static string GetStoreTypeName(
            this DbProviderManifest providerManifest, PrimitiveTypeKind primitiveTypeKind)
        {
            //Contract.Requires(providerManifest != null);

            var edmTypeUsage = TypeUsage.CreateDefaultTypeUsage(
                PrimitiveType.GetEdmPrimitiveType(primitiveTypeKind));

            return providerManifest.GetStoreType(edmTypeUsage).EdmType.Name;
        }
    }
}
