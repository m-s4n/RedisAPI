using Microsoft.EntityFrameworkCore;
using RedisOrnek.Models;

namespace RedisOrnek.Database
{
    public class AppDbContext:DbContext
    {
        // neden ctor yapıyoru ben ef core'un confiğini program.cs den vermek istiyorum.
        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions):base(dbContextOptions)
        {
            
        }
        // onconfiguring ile buradanda yapılır ama biz merkezi yer olan program.cs den yapmak istiyoruz.

        public DbSet<Product> Products { get; set; }

        // Seed Data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasData(
                new Product() { Id = 1, Name = "Milk", Price = 10 },
                new Product() { Id = 2, Name = "Table", Price = 50},
                new Product() { Id= 3, Name = "Computer", Price = 1000},
                new Product() { Id = 4, Name = "whiskey", Price = 500 }
                );
        }
    }
}
