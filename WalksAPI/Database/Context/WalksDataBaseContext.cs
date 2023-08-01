using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WalksAPI.Database.DomainModel;

namespace WalksAPI.Database.Context;

public partial class WalksDataBaseContext : DbContext
{
    public WalksDataBaseContext()
    {
    }

    public WalksDataBaseContext(DbContextOptions<WalksDataBaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Difficulty> Difficulties { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<Region> Regions { get; set; }

    public virtual DbSet<Walk> Walks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=WalksDataBase;Username=postgres;Password=1234");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("uuid-ossp");

        modelBuilder.Entity<Difficulty>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Difficulty_pkey");

            entity.ToTable("Difficulty");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Images_pkey");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Region>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Regions_pkey");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
        });

        modelBuilder.Entity<Walk>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Walks_pkey");

            entity.HasIndex(e => e.DifficultyId, "IX_Walks_DifficultyId");

            entity.HasIndex(e => e.RegionId, "IX_Walks_RegionId");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();

            entity.HasOne(d => d.Difficulty).WithMany(p => p.Walks)
                .HasForeignKey(d => d.DifficultyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("DifficultyId");

            entity.HasOne(d => d.Region).WithMany(p => p.Walks)
                .HasForeignKey(d => d.RegionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("RegionId");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
