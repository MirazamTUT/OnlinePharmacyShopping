﻿using Microsoft.EntityFrameworkCore;
using PharmacyShopping.DataAccess.Models;

namespace PharmacyShopping.DataAccess.DbConnection
{
    public class PharmacyDbContext : DbContext
    {
        public PharmacyDbContext(DbContextOptions<PharmacyDbContext> options) :
            base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<DataBase> DataBases { get; set; }

        public DbSet<Medicine> Medicines { get; set; }

        public DbSet<Pharmacy> Pharmacies { get; set; }

        public DbSet<Purchase> Purchases { get; set; }

        public DbSet<Report> Reports { get; set; }

        public DbSet<ReportMedicine> ReportMedicines { get; set; }

        public DbSet<Sale> Sales { get; set; }

        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ReportMedicine>()
                .HasKey(br => new { br.ReportMedicineId });
            modelBuilder.Entity<ReportMedicine>()
                .HasOne(b => b.Report)
                .WithMany(br => br.ReportMedicines)
                .HasForeignKey(b => b.ReportId);
            modelBuilder.Entity<ReportMedicine>()
                .HasOne(b => b.Medicine)
                .WithMany(br => br.ReportMedicines)
                .HasForeignKey(b => b.MedicineId);
        }
    }
}