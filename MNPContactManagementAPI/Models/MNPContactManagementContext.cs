using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MNPContactManagementAPI.Models
{
    public partial class MNPContactManagementContext : DbContext
    {
        //public MNPContactManagementContext()
        //{
        //}

        public MNPContactManagementContext(DbContextOptions<MNPContactManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ContactDetail> ContactDetail { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=LAPTOP-HA1HHKF0\\LOCAL;Database=MNPContactManagement;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContactDetail>(entity =>
            {
                entity.HasKey(e => e.ContactId);

                entity.Property(e => e.ContactId).HasColumnName("ContactID");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Comments)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ContactName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.EmailAddress)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.JobTitle)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.ContactDetail)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContactDetail_Customer");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.CustomerName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
