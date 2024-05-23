using Bulky.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyWeb.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options) : base(options)
        {
            
        }

        public DbSet<Category>Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Atikul", DisplayOrder = "1" },
                new Category { Id = 2, Name = "Sadikul", DisplayOrder = "2" },
                new Category { Id = 3, Name = "Ismail", DisplayOrder = "3" },
                new Category { Id = 4, Name = "Mominul", DisplayOrder = "4" },
                new Category { Id = 5, Name = "Alamin", DisplayOrder = "5" }

                );


            modelBuilder.Entity<Product>().HasData(
                new Product {
                    Id = 1, 
                    Title = "Fortune of Time",
                    Description = "Present vitaae sodales libre.",
                    ISBN = "SWD32233",
                    ListPrice = 99,
                    Price = 90,
                    Price50 = 85,
                    Author="Atikul",
                    Price100 = 80,
                    CategoryId = 1,
                    ImageUrl=""
                },

                new Product
                {
                    Id = 2,
                    Title = "Dark Skies",
                    Description = "Present vitaae sodales for id 2",
                    ISBN = "CAW77777",
                    ListPrice = 40,
                    Price = 30,
                    Price50 = 25,
                    Author="Sadikul",
                    Price100 = 20,
                    CategoryId = 2,
                    ImageUrl=""

                },

                new Product
                {
                    Id = 3,
                    Title = "Vanish in the sunset",
                    Description = "Julian Button",
                    ISBN = "SWD32233",
                    ListPrice = 55,
                    Price = 50,
                    Price50 = 40,
                    Author="Ismail",
                    Price100 = 35,
                    CategoryId = 3,
                    ImageUrl=""
                }


               );




        }
    }
}
