using Microsoft.EntityFrameworkCore;
using ABM.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ABM.EntityFrameworkCore.Configs
{
    public class TeamConfig : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.ToTable("Teams");

            builder.HasKey(x => x.Id);
        }
    }
}