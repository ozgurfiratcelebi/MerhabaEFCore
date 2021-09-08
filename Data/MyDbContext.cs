using Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace Data
{
    public class MyDbContext : DbContext
    {
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<CustomerProduct> CustomerProduct { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(local); Initial Catalog=MerhabaEFCore;Integrated Security = SSPI");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Turkish_CI_AS");

            modelBuilder.Entity<CustomerProduct>(entity =>
            {
                entity.HasIndex(e => e.CustomerId);

                entity.HasIndex(e => e.ProductId);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerProducts)
                    .HasForeignKey(d => d.CustomerId);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.CustomerProducts)
                    .HasForeignKey(d => d.ProductId);
            });
            modelBuilder.Entity<Product>().HasData(new Product() { Id = 1, Name = "El Feneri" });
            modelBuilder.Entity<Product>().HasData(new Product() { Id = 2, Name = "Çadır" });
            modelBuilder.Entity<Product>().HasData(new Product() { Id = 3, Name = "Uyku Tulumu" });

            base.OnModelCreating(modelBuilder);

        }



    }
}
