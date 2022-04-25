using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class RentACarContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=LAPTOP-MOAUQP2D\SQLEXPRESS;Database=Rentacar;Trusted_Connection=true");
        }

        public DbSet<Car> Cars{ get; set; }
        public DbSet<Segment> Segments{ get; set; }
        public DbSet<Brand> Brands{ get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<CarImage> CarImages { get; set; }


    }
}
