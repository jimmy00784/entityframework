// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.
namespace System.Data.Query.InternalTrees
{
    using System.Data.Metadata.Edm;

    /// <summary>
    /// A computed expression. Defined by a VarDefOp
    /// </summary>
    internal sealed class ComputedVar : Var
    {
        internal ComputedVar(int id, TypeUsage type)
            : base(id, VarType.Computed, type)
        {
        }
    }
}
