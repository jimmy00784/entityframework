// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.
namespace System.Data.Query.InternalTrees
{
    using System.Data.Metadata.Edm;
    using System.Diagnostics;

    /// <summary>
    /// Represents an Element() op - extracts the scalar value from a collection
    /// </summary>
    internal sealed class ElementOp : ScalarOp
    {
        #region constructors

        internal ElementOp(TypeUsage type)
            : base(OpType.Element, type)
        {
        }

        private ElementOp()
            : base(OpType.Element)
        {
        }

        #endregion

        #region public methods

        /// <summary>
        /// Pattern for transformation rules
        /// </summary>
        internal static readonly ElementOp Pattern = new ElementOp();

        /// <summary>
        /// 1 child - collection instance
        /// </summary>
        internal override int Arity
        {
            get { return 1; }
        }

        /// <summary>
        /// Visitor pattern method
        /// </summary>
        /// <param name="v">The BasicOpVisitor that is visiting this Op</param>
        /// <param name="n">The Node that references this Op</param>
        [DebuggerNonUserCode]
        internal override void Accept(BasicOpVisitor v, Node n)
        {
            v.Visit(this, n);
        }

        /// <summary>
        /// Visitor pattern method for visitors with a return value
        /// </summary>
        /// <param name="v">The visitor</param>
        /// <param name="n">The node in question</param>
        /// <returns>An instance of TResultType</returns>
        [DebuggerNonUserCode]
        internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
        {
            return v.Visit(this, n);
        }

        #endregion
    }
}
