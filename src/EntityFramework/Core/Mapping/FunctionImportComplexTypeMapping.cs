// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.
namespace System.Data.Mapping
{
    using System.Collections.ObjectModel;
    using System.Data.Metadata.Edm;

    internal sealed class FunctionImportComplexTypeMapping : FunctionImportStructuralTypeMapping
    {
        internal readonly ComplexType ReturnType;

        internal FunctionImportComplexTypeMapping(
            ComplexType returnType, Collection<FunctionImportReturnTypePropertyMapping> columnsRenameList, LineInfo lineInfo)
            : base(columnsRenameList, lineInfo)
        {
            ReturnType = returnType;
        }
    }
}
