// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.
namespace System.Data.Common.EntitySql
{
    using System.Collections.Generic;
    using System.Data.Metadata.Edm;
    using System.Data.Entity.Resources;
    using System.Diagnostics;

    /// <summary>
    /// Represents an eSQL metadata member expression classified as <see cref="MetadataMemberClass.FunctionGroup"/>.
    /// </summary>
    internal sealed class MetadataFunctionGroup : MetadataMember
    {
        internal MetadataFunctionGroup(string name, IList<EdmFunction> functionMetadata)
            : base(MetadataMemberClass.FunctionGroup, name)
        {
            Debug.Assert(functionMetadata != null && functionMetadata.Count > 0, "FunctionMetadata must not be null or empty");
            FunctionMetadata = functionMetadata;
        }

        internal override string MetadataMemberClassName
        {
            get { return FunctionGroupClassName; }
        }

        internal static string FunctionGroupClassName
        {
            get { return Strings.LocalizedFunction; }
        }

        internal readonly IList<EdmFunction> FunctionMetadata;
    }
}
