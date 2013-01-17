// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.
namespace System.Data.EntityModel.SchemaObjectModel
{
    using System.Collections.Generic;
    using System.Data.Metadata.Edm;
    using System.Text;
    using Som = System.Data.EntityModel.SchemaObjectModel;

    internal abstract class ModelFunctionTypeElement : FacetEnabledSchemaElement
    {
        protected TypeUsage _typeUsage;

        internal ModelFunctionTypeElement(SchemaElement parentElement)
            : base(parentElement)
        {
            _typeUsageBuilder = new TypeUsageBuilder(this);
        }

        internal abstract void WriteIdentity(StringBuilder builder);

        internal abstract TypeUsage GetTypeUsage();

        internal abstract bool ResolveNameAndSetTypeUsage(
            Converter.ConversionCache convertedItemCache, Dictionary<SchemaElement, GlobalItem> newGlobalItems);
    }
}
