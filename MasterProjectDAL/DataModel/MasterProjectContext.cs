using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace MasterProjectDAL.DataModel;

public partial class MasterProjectContext : DbContext
{
    public MasterProjectContext()
    {
    }

    public MasterProjectContext(DbContextOptions<MasterProjectContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Departments> Departments { get; set; }

    public virtual DbSet<Jobs> Jobs { get; set; }

    public virtual DbSet<Locations> Locations { get; set; }

    public virtual DbSet<Login> Login { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=127.0.0.1;port=3306;user=root;password=root;database=jobportaldb", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.32-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Departments>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("departments");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");
        });

        modelBuilder.Entity<Jobs>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("jobs");

            entity.HasIndex(e => e.DepartmentId, "departmentId");

            entity.HasIndex(e => e.LocationId, "locationId");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClosingDate)
                .HasColumnType("datetime")
                .HasColumnName("closingDate");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .HasColumnName("code");
            entity.Property(e => e.DepartmentId).HasColumnName("departmentId");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.LocationId).HasColumnName("locationId");
            entity.Property(e => e.PostedDate)
                .HasColumnType("datetime")
                .HasColumnName("postedDate");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");

            entity.HasOne(d => d.Department).WithMany(p => p.Jobs)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("jobs_ibfk_2");

            entity.HasOne(d => d.Location).WithMany(p => p.Jobs)
                .HasForeignKey(d => d.LocationId)
                .HasConstraintName("jobs_ibfk_1");
        });

        modelBuilder.Entity<Locations>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("locations");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.City)
                .HasMaxLength(100)
                .HasColumnName("city");
            entity.Property(e => e.Country)
                .HasMaxLength(100)
                .HasColumnName("country");
            entity.Property(e => e.State)
                .HasMaxLength(100)
                .HasColumnName("state");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");
            entity.Property(e => e.Zip).HasColumnName("zip");
        });

        modelBuilder.Entity<Login>(entity =>
        {
            entity.HasKey(e => e.Username).HasName("PRIMARY");

            entity.ToTable("login");

            entity.Property(e => e.Password).HasMaxLength(45);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
