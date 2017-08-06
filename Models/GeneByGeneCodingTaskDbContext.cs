using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GeneByGeneCodingTask.Models
{
    public partial class GeneByGeneCodingTaskDbContext : DbContext
    {
        public virtual DbSet<Samples> Samples { get; set; }
        public virtual DbSet<Statuses> Statuses { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        public GeneByGeneCodingTaskDbContext(DbContextOptions<GeneByGeneCodingTaskDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Samples>(entity =>
            {
                entity.HasKey(e => e.SampleId)
                    .HasName("PK__Samples__8B99EC6AFC6858B7");

                entity.Property(e => e.Barcode).HasColumnType("varchar(50)");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.Samples)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK__Samples__Created__403A8C7D");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Samples)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK__Samples__StatusI__412EB0B6");
            });

            modelBuilder.Entity<Statuses>(entity =>
            {
                entity.HasKey(e => e.StatusId)
                    .HasName("PK__Statuses__C8EE2063915E7551");

                entity.Property(e => e.StatusId).ValueGeneratedNever();

                entity.Property(e => e.Status).HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__Users__1788CC4C176341DD");

                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.Property(e => e.FirstName).HasColumnType("varchar(50)");

                entity.Property(e => e.LastName).HasColumnType("varchar(50)");
            });
        }
    }
}