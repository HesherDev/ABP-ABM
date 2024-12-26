using Microsoft.EntityFrameworkCore;
using ABM.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ABM.EntityFrameworkCore.Configs
{
    public class PlayerConfig : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.ToTable("Players");

            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Team)
                   .WithMany(x => x.Players)
                   .HasForeignKey(x => x.TeamId);

        }
    }
}