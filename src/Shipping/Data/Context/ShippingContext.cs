﻿using Microsoft.EntityFrameworkCore;
using ShippingApi.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShippingApi.Data.Context
{
    public class ShippingContext : DbContext
    {
        public ShippingContext( DbContextOptions options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<OutboxEvent> OutboxEvents { get; set; }
    }
}
