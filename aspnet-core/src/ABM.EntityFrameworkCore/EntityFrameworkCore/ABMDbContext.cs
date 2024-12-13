using Abp.Zero.EntityFrameworkCore;
using ABM.Authorization.Roles;
using ABM.Authorization.Users;
using ABM.MultiTenancy;
using Microsoft.EntityFrameworkCore;
using ABM.Entities;
using ABM.EntityFrameworkCore.Configs;

namespace ABM.EntityFrameworkCore;

public class ABMDbContext : AbpZeroDbContext<Tenant, Role, User, ABMDbContext>
{
    /* Define a DbSet for each entity of the application */
    public DbSet<Team> Teams { get; set; } 

    public DbSet<Player> Players { get; set; }

    public ABMDbContext(DbContextOptions<ABMDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new TeamConfig ());
        modelBuilder.ApplyConfiguration(new PlayerConfig());
    }
}
