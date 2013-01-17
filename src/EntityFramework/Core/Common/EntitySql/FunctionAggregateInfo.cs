// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.
namespace System.Data.Common.EntitySql
{
    using System.Data.Common.CommandTrees;
    using System.Data.Common.EntitySql.AST;
    using System.Diagnostics;

    internal sealed class FunctionAggregateInfo : GroupAggregateInfo
    {
        internal FunctionAggregateInfo(
            MethodExpr methodExpr, ErrorContext errCtx, GroupAggregateInfo containingAggregate, ScopeRegion definingScopeRegion)
            : base(GroupAggregateKind.Function, methodExpr, errCtx, containingAggregate, definingScopeRegion)
        {
            Debug.Assert(methodExpr != null, "methodExpr != null");
        }

        internal void AttachToAstNode(string aggregateName, DbAggregate aggregateDefinition)
        {
            Debug.Assert(aggregateDefinition != null, "aggregateDefinition != null");
            base.AttachToAstNode(aggregateName, aggregateDefinition.ResultType);
            AggregateDefinition = aggregateDefinition;
        }

        internal DbAggregate AggregateDefinition;
    }
}
