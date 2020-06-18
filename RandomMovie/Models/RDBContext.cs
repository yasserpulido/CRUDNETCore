using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RandomMovie.Models
{
    public partial class RDBContext : DbContext
    {
        public RDBContext()
        {
        }

        public RDBContext(DbContextOptions<RDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Tmovie> Tmovie { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-4GI7CJV\\SQLEXPRESS;Database=RDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tmovie>(entity =>
            {
                entity.HasKey(e => e.Idmovie);

                entity.ToTable("TMovie");

                entity.Property(e => e.Idmovie).HasColumnName("IDMovie");

                entity.Property(e => e.PickDate).HasColumnType("date");

                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
