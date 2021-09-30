using DealerTrack.DealManagement.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DealerTrack.DealManagement.Persistence
{
    public class DealManagementDbContext: DbContext
    {
        public DealManagementDbContext(DbContextOptions<DealManagementDbContext> options)
           : base(options)
        {

        }
        public DbSet<Deal> Deals { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Dealership> Dealerships { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DealManagementDbContext).Assembly);
           
            modelBuilder.Entity<Deal>(deal =>
            {
                deal.HasKey(x => x.Id);
                deal.Property(x => x.Id).ValueGeneratedOnAdd().UseSqlServerIdentityColumn();
                deal.Property(x => x.Date);
                deal.Property(x => x.DealNumber).IsRequired();
                deal.Property(x => x.Price).HasColumnType("decimal(18,4)"); 
                deal.Property(x => x.CustomerName);
              
            });
            modelBuilder.Entity<Vehicle>(vehicle =>
            {
                vehicle.HasKey(x => x.VehicleID);
                vehicle.Property(x => x.VehicleID).ValueGeneratedOnAdd().UseSqlServerIdentityColumn();
               
                vehicle.Property(x => x.VehicleName);
              

            });
            modelBuilder.Entity<Dealership>(dealership =>
            {
                dealership.HasKey(x => new { x.DealerID });
                dealership.Property(x => x.DealerID).ValueGeneratedOnAdd().UseSqlServerIdentityColumn();
                dealership.Property(x => x.DealershipName);

            });
        }

    }
}
