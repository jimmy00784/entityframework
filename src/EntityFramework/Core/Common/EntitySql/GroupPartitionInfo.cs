// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.
namespace System.Data.Common.EntitySql
{
    using System.Data.Common.CommandTrees;
    using System.Data.Common.EntitySql.AST;
    using System.Diagnostics;

    internal sealed class GroupPartitionInfo : GroupAggregateInfo
    {
        internal GroupPartitionInfo(
            GroupPartitionExpr groupPartitionExpr, ErrorContext errCtx, GroupAggregateInfo containingAggregate,
            ScopeRegion definingScopeRegion)
            : base(GroupAggregateKind.Partition, groupPartitionExpr, errCtx, containingAggregate, definingScopeRegion)
        {
            Debug.Assert(groupPartitionExpr != null, "groupPartitionExpr != null");
        }

        internal void AttachToAstNode(string aggregateName, DbExpression aggregateDefinition)
        {
            Debug.Assert(aggregateDefinition != null, "aggregateDefinition != null");
            base.AttachToAstNode(aggregateName, aggregateDefinition.ResultType);
            AggregateDefinition = aggregateDefinition;
        }

        internal DbExpression AggregateDefinition;
    }
}
