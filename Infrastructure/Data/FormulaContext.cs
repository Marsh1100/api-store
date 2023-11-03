using System;
using System.Collections.Generic;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public partial class FormulaContext : DbContext
{
    public FormulaContext()
    {
    }

    public FormulaContext(DbContextOptions<FormulaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Driver> Drivers { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

  
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Driver>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("driver");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(60)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("team");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("name");

            entity.HasMany(d => d.IdDrivers).WithMany(p => p.IdTeams)
                .UsingEntity<Dictionary<string, object>>(
                    "Teamdriver",
                    r => r.HasOne<Driver>().WithMany()
                        .HasForeignKey("IdDriver")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_team_driver_driver_idDriver"),
                    l => l.HasOne<Team>().WithMany()
                        .HasForeignKey("IdTeam")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_team_driver_team_idTeam"),
                    j =>
                    {
                        j.HasKey("IdTeam", "IdDriver")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("teamdriver");
                        j.HasIndex(new[] { "IdDriver" }, "FK_team_driver_driver_idDriver");
                        j.HasIndex(new[] { "IdTeam" }, "IX_team_driver_IdteamFk");
                        j.IndexerProperty<int>("IdTeam").HasColumnName("idTeam");
                        j.IndexerProperty<int>("IdDriver").HasColumnName("idDriver");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
