﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderApi.Domain.Dtos.Request
{
    public class ProductRequestDto
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }
    }
}