// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.
namespace System.Data.Objects
{
    using System.ComponentModel;

    internal interface IObjectView
    {
        void EntityPropertyChanged(object sender, PropertyChangedEventArgs e);
        void CollectionChanged(object sender, CollectionChangeEventArgs e);
    }
}
