﻿using Core.Entities.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Category: BaseEntity
    {
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";

    }
}
