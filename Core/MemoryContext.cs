using Core.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Core
{
    public class MemoryContext : DbContext
    {
        public MemoryContext(DbContextOptions<MemoryContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<Buyer>()
                .Property(p => p.SalesIds)
                .HasConversion(v => JsonConvert.SerializeObject(v), v => JsonConvert.DeserializeObject<List<int>>(v));
        }

        public DbSet<Buyer> Buyers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProvidedProduct> ProvidedProducts { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleData> SalesData { get; set; }
        public DbSet<SalesPoint> SalesPoints { get; set; }
    }
}
