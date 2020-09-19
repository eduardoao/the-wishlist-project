using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Wishlist.Core.Models;

namespace Wishlist.Data
{
    public class PostgreContext: DbContext
    {
        public PostgreContext()
        {

        }

        public PostgreContext(DbContextOptions<PostgreContext> options) : base(options) { }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<WishClient> WishClients { get; set; }
        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DateCreate") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("DateCreate").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DateCreate").IsModified = false;
                }
            }

            return base.SaveChanges();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
          => optionsBuilder.UseNpgsql("Host=ec2-52-207-124-89.compute-1.amazonaws.com;Database=d9c0bata6ln7f3;Username=emigelmvmmvkzc;Password=c93cb439fc95909c4938b5d8e5ee18330d5392074ad9d31337fcf05080e0f6c2");
    }

}

