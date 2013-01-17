// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.
namespace System.Data.Common.CommandTrees
{
    using System.Data.Common.CommandTrees.Internal;
    using System.Data.Metadata.Edm;

    /// <summary>
    /// The aggregate type that corresponds to exposing the collection of elements that comprise a group
    /// </summary>
    public sealed class DbGroupAggregate : DbAggregate
    {
        internal DbGroupAggregate(TypeUsage resultType, DbExpressionList arguments)
            : base(resultType, arguments)
        {
        }
    }
}
