// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.
namespace System.Data.Mapping
{
    using System.Data.Metadata.Edm;

    /// <summary>
    /// Mapping metadata for all OC member maps.
    /// </summary>
    internal class ObjectNavigationPropertyMapping : ObjectMemberMapping
    {
        #region Constructors

        /// <summary>
        /// Constrcut a new member mapping metadata object
        /// </summary>
        /// <param name="edmNavigationProperty"></param>
        /// <param name="clrNavigationProperty"></param>
        internal ObjectNavigationPropertyMapping(NavigationProperty edmNavigationProperty, NavigationProperty clrNavigationProperty)
            :
                base(edmNavigationProperty, clrNavigationProperty)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// return the member mapping kind
        /// </summary>
        internal override MemberMappingKind MemberMappingKind
        {
            get { return MemberMappingKind.NavigationPropertyMapping; }
        }

        #endregion
    }
}
