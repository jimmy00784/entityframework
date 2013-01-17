// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.
namespace System.Data.Common.EntitySql
{
    internal enum ScopeEntryKind
    {
        SourceVar,
        GroupKeyDefinition,
        ProjectionItemDefinition,
        FreeVar,

        /// <summary>
        /// Represents a group input scope entry that should no longer be referenced. 
        /// </summary>
        InvalidGroupInputRef
    }
}
