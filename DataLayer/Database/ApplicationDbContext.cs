using Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Database
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Invoice>()
                .HasKey(e => e.InvoiceID);

            modelBuilder.Entity<Invoice>()
    .HasMany(i => i.Items)
    .WithOne(i => i.Invoice)
    .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Contact>()
            .HasOne(c => c.User)
            .WithMany(u => u.Contacts)
            .HasForeignKey(c => c.UserId)
            .IsRequired();

            modelBuilder.Entity<Contact>()
                .HasKey(e => e.ContactId);
                
            modelBuilder.Entity<Invoice>()
                .Property(e => e.TotalPrice)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Item>()
                .HasKey(d => d.Id);
                
            modelBuilder.Entity<Item>()
                .Property(e => e.UnitPrice)
                .IsRequired()
                .HasColumnType("decimal(18,2)"); // Configure the Price property as decimal(18,2)
            modelBuilder.Entity<Item>()
                .Property(e => e.Amount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Subscription>()
                .HasKey(e => e.Id);

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Item> Items { get; set; }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Subscription> Subscriptions { get; set; }
    }
}
