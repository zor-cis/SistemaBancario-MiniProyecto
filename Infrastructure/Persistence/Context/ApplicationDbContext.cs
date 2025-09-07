using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Client> Client { get; set; }
        public DbSet<Account> Accounts { get; set; }

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

            modelBuilder.Entity<Account>(en => 
            {
                en.HasKey(c => c.AccountNumber);
                en.Property(c => c.AccountNumber).HasColumnName("AccountNumber").IsRequired().HasMaxLength(20);
                en.Property(c => c.TypeAccount).HasColumnName("TypeAccount").IsRequired();
                en.Property(c => c.HolderAccount).HasColumnName("HolderAccount").IsRequired().HasMaxLength(100);
                en.Property(c => c.IdClient).HasColumnName("ClientId").IsRequired();
                en.Property(c => c.Balance).HasColumnName("Balance").IsRequired().HasColumnType("decimal(18,2)").HasDefaultValue(0);
                en.Property(c => c.CreatedAt).HasColumnName("CreatedAt").IsRequired();
                en.Property(c => c.isActive).HasColumnName("isActive").HasDefaultValue(true);
                en.HasOne(a => a.Client).WithMany(c => c.Accounts).HasForeignKey(a => a.IdClient);
                en.HasDiscriminator<AccountType>("TypeAccount").HasValue<SavingsAccount>(AccountType.Saving).HasValue<CurrentAcount>(AccountType.Current);
            });
        }

    }
}
