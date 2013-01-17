// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.
namespace System.Data.Objects.ELinq
{
    using System.Data.Common.CommandTrees;
    using System.Diagnostics.Contracts;
    using System.Linq.Expressions;

    /// <summary>
    /// Class describing a LINQ parameter and its bound expression. For instance, in
    /// 
    /// products.Select(p => p.ID)
    /// 
    /// the 'products' query is the bound expression, and 'p' is the parameter.
    /// </summary>
    internal sealed class Binding
    {
        internal Binding(Expression linqExpression, DbExpression cqtExpression)
        {
            Contract.Requires(linqExpression != null);
            Contract.Requires(cqtExpression != null);
            LinqExpression = linqExpression;
            CqtExpression = cqtExpression;
        }

        internal readonly Expression LinqExpression;
        internal readonly DbExpression CqtExpression;
    }
}
