﻿using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using NSE.ShoppingCart.API.Model;

namespace NSE.ShoppingCart.API.Data
{
    public class CartContext : DbContext
    {
        public CartContext(DbContextOptions<CartContext> options)
            : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<CartItem> CartItems { get; set; }

        public DbSet<CartCustomer> CartCustomer { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                    .Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            modelBuilder.Ignore<ValidationResult>();
            
            modelBuilder.Entity<CartCustomer>()
                .HasIndex(c => c.CustomerId)
                .HasDatabaseName("IDX_Customer");

            modelBuilder.Entity<CartCustomer>()
                .HasMany(c => c.Items)
                .WithOne(i => i.CartCustomer)
                .HasForeignKey(i => i.CartId);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
        }
    }
}
