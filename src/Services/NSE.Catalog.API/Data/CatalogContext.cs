﻿using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using NSE.Catalog.API.Models;
using NSE.Core.Data;
using NSE.Core.Messages;

namespace NSE.Catalog.API.Data
{
    public class CatalogContext : DbContext , IUnitOfWork
    {
        public CatalogContext(DbContextOptions<CatalogContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<ValidationResult>();
            modelBuilder.Ignore<Event>();
            
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                    .Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");  //Seleciona todas as propriedades do tipo string e muda o tipo para varchar(100), caso ela nao esteja mapeada


            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogContext).Assembly);
        }

        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }
    }
}
