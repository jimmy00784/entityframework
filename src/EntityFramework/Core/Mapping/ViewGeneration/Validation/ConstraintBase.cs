// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.
namespace System.Data.Mapping.ViewGeneration.Validation
{
    using System.Data.Common.Utils;
    using System.Data.Mapping.ViewGeneration.Structures;
    using WrapperBoolExpr = System.Data.Common.Utils.Boolean.BoolExpr<Structures.LeftCellWrapper>;
    using WrapperTreeExpr = System.Data.Common.Utils.Boolean.TreeExpr<Structures.LeftCellWrapper>;
    using WrapperAndExpr = System.Data.Common.Utils.Boolean.AndExpr<Structures.LeftCellWrapper>;
    using WrapperOrExpr = System.Data.Common.Utils.Boolean.OrExpr<Structures.LeftCellWrapper>;
    using WrapperNotExpr = System.Data.Common.Utils.Boolean.NotExpr<Structures.LeftCellWrapper>;
    using WrapperTermExpr = System.Data.Common.Utils.Boolean.TermExpr<Structures.LeftCellWrapper>;
    using WrapperTrueExpr = System.Data.Common.Utils.Boolean.TrueExpr<Structures.LeftCellWrapper>;
    using WrapperFalseExpr = System.Data.Common.Utils.Boolean.FalseExpr<Structures.LeftCellWrapper>;

    // A superclass for constraint errors. It also contains useful constraint
    // checking methods
    internal abstract class ConstraintBase : InternalBase
    {
        #region Methods

        // effects: Returns an error log record with this constraint's information
        internal abstract ErrorLog.Record GetErrorRecord();

        #endregion
    }
}
