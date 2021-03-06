﻿// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.
namespace AllTypeKeysModel
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class DateTimeKeyEntity
    {
        [Key]
        public DateTime key { get; set; }
        public string Description { get; set; }
    }
}
