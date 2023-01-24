using Core.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Core
{
    public class MemoryContext : DbContext
    {
        public MemoryContext(DbContextOptions<MemoryContext> options)
            : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
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

            modelBuilder
                .Entity<Sale>()
                .Property(p => p.SalesData)
                .HasConversion(v => JsonConvert.SerializeObject(v), v => JsonConvert.DeserializeObject<List<SaleData>>(v));

            modelBuilder
                .Entity<SalesPoint>()
                .Property(p => p.ProvidedProducts)
                .HasConversion(v => JsonConvert.SerializeObject(v), v => JsonConvert.DeserializeObject<List<ProvidedProduct>>(v));

            modelBuilder.Entity<Buyer>().HasData(
                new Buyer[]
                {
                    new Buyer { Id = 1, Name = "Афанасьев Андрей", SalesIds = new List<int> { 1, 3 } },
                    new Buyer { Id = 2, Name = "Исмагилова Ляйсан", SalesIds = new List<int> { 4 } },
                    new Buyer { Id = 3, Name = "Прохоров Павел", SalesIds = new List<int> { 2, 5 } },
                });

            modelBuilder.Entity<Product>().HasData(
                new Product[]
                {
                    new Product { Id = 1, Name = "Кофе", Price = 60 },
                    new Product { Id = 2, Name = "Жвачка", Price = 20.60m },
                    new Product { Id = 3, Name = "Энергетик", Price = 54.99m }
                });

            modelBuilder.Entity<SalesPoint>().HasData(
                new SalesPoint[]
                {
                    new SalesPoint
                    {
                        Id = 1,
                        Name = "Точка 1",
                        ProvidedProducts = new List<ProvidedProduct>
                        {
                            new ProvidedProduct { ProductId = 1, ProductQuantity = 12 },
                            new ProvidedProduct { ProductId = 2, ProductQuantity = 35 },
                            new ProvidedProduct { ProductId = 3, ProductQuantity = 29 }
                        }
                    },
                    new SalesPoint
                    {
                        Id = 2,
                        Name = "Точка 2",
                        ProvidedProducts = new List<ProvidedProduct>
                        {
                            new ProvidedProduct { ProductId = 1, ProductQuantity = 74 },
                            new ProvidedProduct { ProductId = 2, ProductQuantity = 5 },
                            new ProvidedProduct { ProductId = 3, ProductQuantity = 13 }
                        }
                    },
                    new SalesPoint
                    {
                        Id = 3,
                        Name = "Точка 3",
                        ProvidedProducts = new List<ProvidedProduct>
                        {
                            new ProvidedProduct { ProductId = 1, ProductQuantity = 91 },
                            new ProvidedProduct { ProductId = 2, ProductQuantity = 123 },
                            new ProvidedProduct { ProductId = 3, ProductQuantity = 23 }
                        }
                    },
                });

            modelBuilder.Entity<Sale>().HasData(
                new Sale[]
                {
                    new Sale
                    {
                        Id = 1,
                        BuyerId = 1,
                        SalesPointId = 1,
                        DateTime = DateTime.Now,
                        SalesData = new List<SaleData>
                        {
                            new SaleData { ProductId = 1, ProductQuantity = 2, ProductIdAmount = 120 }
                        },
                        TotalAmount = 120
                    },
                    new Sale
                    {
                        Id = 2,
                        BuyerId = 3,
                        SalesPointId = 2,
                        DateTime = DateTime.Now,
                        SalesData = new List<SaleData>
                        {
                            new SaleData { ProductId = 1, ProductQuantity = 1, ProductIdAmount = 60 },
                            new SaleData { ProductId = 2, ProductQuantity = 1, ProductIdAmount = 20.60m },
                        },
                        TotalAmount = 80.60m
                    },
                    new Sale
                    {
                        Id = 3,
                        BuyerId = 1,
                        SalesPointId = 3,
                        DateTime = DateTime.Now,
                        SalesData = new List<SaleData>
                        {
                            new SaleData { ProductId = 2, ProductQuantity = 1, ProductIdAmount = 20.60m },
                            new SaleData { ProductId = 3, ProductQuantity = 2, ProductIdAmount = 109.98m }
                        },
                        TotalAmount = 130.58m
                    },
                    new Sale
                    {
                        Id = 4,
                        BuyerId = 2,
                        SalesPointId = 1,
                        DateTime = DateTime.Now,
                        SalesData = new List<SaleData>
                        {
                            new SaleData { ProductId = 1, ProductQuantity = 1, ProductIdAmount = 60 },
                            new SaleData { ProductId = 2, ProductQuantity = 1, ProductIdAmount = 20.60m },
                        },
                        TotalAmount = 120
                    },
                    new Sale
                    {
                        Id = 5,
                        BuyerId = 3,
                        SalesPointId = 2,
                        DateTime = DateTime.Now,
                        SalesData = new List<SaleData>
                        {
                            new SaleData { ProductId = 3, ProductQuantity = 4, ProductIdAmount = 219.96m }
                        },
                        TotalAmount = 219.96m
                    }
                });
        }

        public DbSet<Buyer> Buyers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SalesPoint> SalesPoints { get; set; }
    }
}
