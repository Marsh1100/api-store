
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Infrastructure.Configurations;

public class TeamConfiguration : IEntityTypeConfiguration<Team>
{
    public void Configure(EntityTypeBuilder<Team> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.ToTable("team");

        builder.HasIndex(e => e.Name, "team_name_IU").IsUnique();

        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnName("name");

        builder.HasMany(d => d.IdDrivers).WithMany(p => p.IdTeams)
            .UsingEntity<Dictionary<string, object>>(
                "Teamdriver",
                r => r.HasOne<Driver>().WithMany()
                    .HasForeignKey("IdDriver")
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("idDriver"),
                l => l.HasOne<Team>().WithMany()
                    .HasForeignKey("IdTeam")
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("idTeam"),
                j =>
                {
                    j.HasKey("IdTeam", "IdDriver")
                        .HasName("PRIMARY")
                        .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                    j.ToTable("teamdriver");
                    j.HasIndex(new[] { "IdDriver" }, "idDriver_idx");
                    j.HasIndex(new[] { "IdTeam" }, "idTeam_idx");
                    j.IndexerProperty<int>("IdTeam").HasColumnName("idTeam");
                    j.IndexerProperty<int>("IdDriver").HasColumnName("idDriver");
                });
    }

}