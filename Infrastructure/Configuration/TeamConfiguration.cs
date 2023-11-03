
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

        builder
            .HasMany(p=>p.Drivers)
            .WithMany(p=>p.Teams)
            .UsingEntity<TeamDriver>(
                j=> j
                    .HasOne(t=> t.Driver)
                    .WithMany(m=> m.TeamDrivers)
                    .HasForeignKey(f=> f.IdDriver),
                j=> j
                    .HasOne(t=> t.Team)
                    .WithMany(m=>m.TeamDrivers)
                    .HasForeignKey(f=> f.IdTeam),
                j=>
                {
                    j.ToTable("teamdriver");
                    j.HasKey(t=> new {t.IdDriver, t.IdTeam});
                }
            );
    }

}