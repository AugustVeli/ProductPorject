using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
     public DbSet<Client> Clients { get; set; }
     public DbSet<Product> Products { get; set; }
     public DbSet<Order> Orders { get; set; }
     
     protected override void OnModelCreating(ModelBuilder modelBuilder)
     {
         modelBuilder.Entity<Client>().HasData(
             new Client()
             {
                 Id = 1,
                 Name = "Marija",
                 Email = "marija@gmail.com",
                 Birthdate = new DateTime(2000,5,12),
                 Gender = Gender.Female,
             },
             new Client()
             {
                 Id = 2,
                 Name = "Masha",
                 Email = "masha@example.com",
                 Birthdate = new DateTime(2005,7,22),
                 Gender = Gender.Female,
             },
             new Client()
             {
                 Id = 3,
                 Name = "Pasha",
                 Email = "pasha@example.com",
                 Birthdate = new DateTime(2008,9,3),
                 Gender = Gender.Male,
             }
         );
         
         modelBuilder.Entity<Product>().HasData(
             new Product()
             {
                 Id = 1,
                 Code = "asdfgx12",
                 Title = "Microphon",
                 Price = Convert.ToDecimal(100.99)
             },
             new Product()
             {
                 Id = 2,
                 Code = "asdfgg12",
                 Title = "Doll",
                 Price = Convert.ToDecimal(26.36)
             },
             new Product()
             {
                 Id = 3,
                 Code = "afffgg12",
                 Title = "Car",
                 Price = Convert.ToDecimal(50.66)
             }
         );
         
         modelBuilder.Entity<Order>().HasData(
             new Order()
             {
                 Id = 1,
                 ClientId = 1,
                 ProductId = 1,
                 Quantity = 3,
                 Status = Status.Paid
             },
             new Order()
             {
                 Id = 2,
                 ClientId = 1,
                 ProductId = 2,
                 Quantity = 4,
                 Status = Status.Created
             },
            new Order()
             {
                 Id = 3,
                 ClientId = 1,
                 ProductId = 3,
                 Quantity = 6,
                 Status = Status.Delivered
             },
             // client2
            new Order()
             {
                 Id = 4,
                 ClientId = 2,
                 ProductId = 2,
                 Quantity = 3,
                 Status = Status.Created
             },
            new Order()
            {
                Id = 5,
                ClientId = 2,
                ProductId = 1,
                Quantity = 5,
                Status = Status.Delivered
            },
             // client3
            new Order()
            {
                Id = 6,
                ClientId = 3,
                ProductId = 3,
                Quantity = 2,
                Status = Status.Paid
            }
         );
     }

}