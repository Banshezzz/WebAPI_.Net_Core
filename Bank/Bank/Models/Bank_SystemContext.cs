using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Bank_System.Models
{
    public partial class Bank_SystemContext : DbContext
    {
        public Bank_SystemContext()
        {
        }

        public Bank_SystemContext(DbContextOptions<Bank_SystemContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<Bank> Banks { get; set; } = null!;
        public virtual DbSet<Passport> Passports { get; set; } = null!;
        public virtual DbSet<Supporter> Supporters { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=BANSHEE;Initial Catalog=Bank_System;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.Username)
                    .HasName("PK_Account_1");

                entity.ToTable("Account");

                entity.Property(e => e.Username).HasMaxLength(50);

                entity.Property(e => e.Address).HasMaxLength(50);

                entity.Property(e => e.Birthday).HasColumnType("date");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.PassportId)
                    .HasMaxLength(20)
                    .IsFixedLength();

                entity.Property(e => e.PasswordHash).HasMaxLength(200);

                entity.Property(e => e.PasswordSalt).HasMaxLength(200);

                entity.HasOne(d => d.BankCodeNavigation)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.BankCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Account_Bank");
            });

            modelBuilder.Entity<Bank>(entity =>
            {
                entity.ToTable("Bank");

                entity.Property(e => e.BankName).HasMaxLength(50);
            });

            modelBuilder.Entity<Passport>(entity =>
            {
                entity.HasKey(e => e.Username);

                entity.ToTable("Passport");

                entity.HasIndex(e => e.Username, "IX_Passport")
                    .IsUnique();

                entity.Property(e => e.Username).HasMaxLength(50);

                entity.Property(e => e.Back).HasMaxLength(100);

                entity.Property(e => e.Front).HasMaxLength(100);

                entity.HasOne(d => d.UsernameNavigation)
                    .WithOne(p => p.Passport)
                    .HasForeignKey<Passport>(d => d.Username)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Passport_Account");
            });

            modelBuilder.Entity<Supporter>(entity =>
            {
                entity.ToTable("Supporter");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Username).HasMaxLength(50);

                entity.HasOne(d => d.BankCodeNavigation)
                    .WithMany(p => p.Supporters)
                    .HasForeignKey(d => d.BankCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Supporter_Bank");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
