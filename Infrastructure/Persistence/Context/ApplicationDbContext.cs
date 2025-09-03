using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Client> Client { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Client>(en => 
            {
                en.HasKey(c => c.Id);
                en.Property(c => c.Name).HasColumnName("name").IsRequired().HasMaxLength(50);
                en.Property(c => c.LastName).HasColumnName("last_name").IsRequired().HasMaxLength(50);
                en.Property(c => c.DocumentTypeId).HasColumnName("documentTypeId").IsRequired();
                en.Property(c => c.Email).IsRequired().HasColumnName("email").HasMaxLength(150);
                en.Property(c => c.PhoneNumber).HasColumnName("phone_number");
                en.Property(c => c.DateOfBirth).HasColumnName("dateOfBirth").IsRequired();
                en.Property(c => c.Password).HasColumnName("password").IsRequired().HasMaxLength(255);
                en.Property(c => c.Age).HasColumnName("age");

            });
        }

    }
}
