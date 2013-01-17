﻿// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.
using System.Diagnostics.CodeAnalysis;

[assembly:
    SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace",
        Target = "System.Data.Entity.Migrations.Sql")]
[assembly:
    SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace",
        Target = "System.Data.Entity.ModelConfiguration")]
[assembly: SuppressMessage("Microsoft.Design", "CA2210:AssembliesShouldHaveValidStrongNames")]
[assembly:
    SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace",
        Target = "System.Data.Entity.Validation")]
[assembly:
    SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace",
        Target = "System.Data.Entity.Migrations.Utilities")]
[assembly:
    SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace",
        Target = "System.Data.Entity.Migrations.History")]
[assembly:
    SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace",
        Target = "System.Data.Entity.Migrations.Builders")]
[assembly: SuppressMessage("Microsoft.Usage", "CA2243:AttributeStringLiteralsShouldParseCorrectly")]
[assembly:
    SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Scope = "member",
        Target = "System.Data.Entity.ModelConfiguration.Conventions.Sets.V1ConventionSet.#.cctor()")]
[assembly:
    SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Scope = "member",
        Target =
            "System.Data.Entity.ModelConfiguration.Conventions.ForeignKeyDiscoveryConvention.#System.Data.Entity.ModelConfiguration.Conventions.IEdmConvention`1<System.Data.Entity.Edm.EdmAssociationType>.Apply(System.Data.Entity.Edm.EdmAssociationType,System.Data.Entity.Edm.EdmModel)"
        )]
[assembly:
    SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Scope = "member",
        Target = "System.Data.Entity.Edm.Validation.Internal.EdmModel.EdmModelSyntacticValidationRules.#.cctor()")]
[assembly:
    SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Scope = "member",
        Target = "System.Data.Entity.Edm.Validation.Internal.EdmModel.EdmModelSemanticValidationRules.#.cctor()")]
[assembly:
    SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Scope = "member",
        Target = "System.Data.Entity.Edm.Validation.Internal.EdmModel.EdmModelSemanticValidationRules.#.cctor()")]
[assembly:
    SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Scope = "member",
        Target = "System.Data.Entity.Edm.Validation.Internal.EdmModel.EdmModelSemanticValidationRules.#.cctor()")]
[assembly:
    SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace",
        Target = "System.Data.Common.CommandTrees.ExpressionBuilder")]
[assembly:
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "System.Console.WriteLine(System.String)", Scope = "member",
        Target = "System.Data.Common.EntitySql.CqlParser.#dump_stacks(System.Int32)")]
[assembly:
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "System.Data.EntityModel.SchemaObjectModel.ScalarType.ConvertToByteArray(System.String)", Scope = "member",
        Target = "System.Data.Metadata.Edm.MetadataAssemblyHelper.#.cctor()")]
[assembly:
    SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
        MessageId = "System.Data.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)", Scope = "member",
        Target =
            "System.Data.Query.PlanCompiler.PreProcessor.#ExpandView(System.Data.Query.InternalTrees.Node,System.Data.Query.InternalTrees.ScanTableOp,System.Data.Query.InternalTrees.IsOfOp&)"
        )]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1703:ResourceStringsShouldBeSpelledCorrectly", MessageId = "Def", Scope = "resource",
        Target = "System.Data.Entity.Properties.Resources.resources")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1703:ResourceStringsShouldBeSpelledCorrectly", MessageId = "dddddddd-dddd-dddd-dddd-dddddddddddd"
        , Scope = "resource", Target = "System.Data.Entity.Properties.Resources.resources")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1703:ResourceStringsShouldBeSpelledCorrectly", MessageId = "Deref", Scope = "resource",
        Target = "System.Data.Entity.Properties.Resources.resources")]
[assembly:
    SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Scope = "member",
        Target =
            "System.Data.Mapping.StorageMappingItemCollection+ViewDictionary.#GetGeneratedView(System.Data.Metadata.Edm.EntitySetBase,System.Data.Metadata.Edm.MetadataWorkspace,System.Data.Mapping.StorageMappingItemCollection)"
        )]
[assembly:
    SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member",
        Target = "System.Data.Common.EntitySql.CqlLexer.#yy_double(System.Char[])")]
[assembly:
    SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member",
        Target = "System.Data.Common.EntitySql.CqlLexer.#yy_error(System.Int32,System.Boolean)")]
[assembly:
    SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member",
        Target = "System.Data.Common.EntitySql.CqlLexer.#IsCanonicalFunctionCall(System.String,System.Char)")]
[assembly:
    SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Scope = "member",
        Target =
            "System.Data.Objects.CompiledQuery.#Compile`17(System.Linq.Expressions.Expression`1<System.Func`17<!!0,!!1,!!2,!!3,!!4,!!5,!!6,!!7,!!8,!!9,!!10,!!11,!!12,!!13,!!14,!!15,!!16>>)"
        )]
[assembly:
    SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Scope = "member",
        Target =
            "System.Data.Objects.CompiledQuery.#Compile`16(System.Linq.Expressions.Expression`1<System.Func`16<!!0,!!1,!!2,!!3,!!4,!!5,!!6,!!7,!!8,!!9,!!10,!!11,!!12,!!13,!!14,!!15>>)"
        )]
[assembly:
    SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Scope = "member",
        Target =
            "System.Data.Objects.CompiledQuery.#Compile`15(System.Linq.Expressions.Expression`1<System.Func`15<!!0,!!1,!!2,!!3,!!4,!!5,!!6,!!7,!!8,!!9,!!10,!!11,!!12,!!13,!!14>>)"
        )]
[assembly:
    SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Scope = "member",
        Target =
            "System.Data.Objects.CompiledQuery.#Compile`14(System.Linq.Expressions.Expression`1<System.Func`14<!!0,!!1,!!2,!!3,!!4,!!5,!!6,!!7,!!8,!!9,!!10,!!11,!!12,!!13>>)"
        )]
[assembly:
    SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Scope = "member",
        Target =
            "System.Data.Objects.CompiledQuery.#Compile`13(System.Linq.Expressions.Expression`1<System.Func`13<!!0,!!1,!!2,!!3,!!4,!!5,!!6,!!7,!!8,!!9,!!10,!!11,!!12>>)"
        )]
[assembly:
    SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Scope = "member",
        Target =
            "System.Data.Objects.CompiledQuery.#Compile`12(System.Linq.Expressions.Expression`1<System.Func`12<!!0,!!1,!!2,!!3,!!4,!!5,!!6,!!7,!!8,!!9,!!10,!!11>>)"
        )]
[assembly:
    SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Scope = "member",
        Target =
            "System.Data.Objects.CompiledQuery.#Compile`11(System.Linq.Expressions.Expression`1<System.Func`11<!!0,!!1,!!2,!!3,!!4,!!5,!!6,!!7,!!8,!!9,!!10>>)"
        )]
[assembly:
    SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Scope = "member",
        Target =
            "System.Data.Objects.CompiledQuery.#Compile`10(System.Linq.Expressions.Expression`1<System.Func`10<!!0,!!1,!!2,!!3,!!4,!!5,!!6,!!7,!!8,!!9>>)"
        )]
[assembly:
    SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Scope = "member",
        Target = "System.Data.Common.EntitySql.CqlLexer.#yy_error_string")]
[assembly:
    SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Scope = "member",
        Target = "System.Data.Common.EntitySql.CqlLexer.#_parserOptions")]
[assembly:
    SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Scope = "member",
        Target = "System.Data.Common.EntitySql.CqlParser.#yyrule")]
[assembly:
    SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "code", Scope = "member",
        Target = "System.Data.Common.EntitySql.CqlLexer.#yy_error(System.Int32,System.Boolean)")]
[assembly:
    SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Scope = "member",
        Target = "System.Data.Common.EntitySql.CqlParser.#YYMAJOR")]
[assembly:
    SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Scope = "member",
        Target = "System.Data.Common.EntitySql.CqlParser.#YYMINOR")]
[assembly:
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target = "System.Data.Common.EntitySql.CqlLexer.#yybegin(System.Int32)")]
[assembly:
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target = "System.Data.Common.EntitySql.CqlLexer.#yylength()")]
[assembly:
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target = "System.Data.Common.EntitySql.CqlParser.#debug(System.String)")]
[assembly:
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target = "System.Data.Common.EntitySql.CqlParser.#dump_stacks(System.Int32)")]
[assembly:
    SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member",
        Target = "System.Data.Common.EntitySql.CqlParser.#yylexdebug(System.Int32,System.Int32)")]
[assembly:
    SuppressMessage("Microsoft.Performance", "CA1814:PreferJaggedArraysOverMultidimensional", MessageId = "Member", Scope = "member",
        Target = "System.Data.Common.EntitySql.CqlLexer.#yy_nxt")]
[assembly:
    SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Scope = "member",
        Target = "System.Data.Common.EntitySql.CqlParser.#yyparse()")]
[assembly:
    SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace",
        Target = "System.Data.Mapping")]
[assembly:
    SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace",
        Target = "System.Data.Objects.SqlClient")]
[assembly:
    SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace",
        Target = "System.Data.Common.EntitySql")]
[assembly:
    SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace",
        Target = "System.Data.Common.CommandTrees.ExpressionBuilder.Spatial")]
[assembly:
    SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Scope = "type",
        Target = "System.Data.Common.EntitySql.CqlLexer")]
[assembly:
    SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Scope = "member",
        Target = "System.Data.Common.EntitySql.CqlParser.#yyparse()")]
[assembly:
    SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Scope = "member",
        Target = "System.Data.Common.EntitySql.CqlParser.#yyparse()")]
[assembly: 
    SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode", Scope = "member",
        Target = "System.Data.Entity.IQueryableExtensions.#.cctor()")]
[assembly: 
    SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Scope = "member",
        Target = "System.Data.Entity.IQueryableExtensions.#.cctor()")]
