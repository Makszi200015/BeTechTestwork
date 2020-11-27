using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace BeTechTestwork
{
    public partial class InventoryControlContext : DbContext
    {
        public InventoryControlContext()
        {
        }

        public InventoryControlContext(DbContextOptions<InventoryControlContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Currency> Currency { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Warehouse> Warehouses { get; set; }
        public virtual DbSet<WarehouseProduct> WarehouseProducts { get; set; }

      

    
    }
}
