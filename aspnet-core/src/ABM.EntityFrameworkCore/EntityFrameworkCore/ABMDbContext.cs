using Abp.Zero.EntityFrameworkCore;
using ABM.Authorization.Roles;
using ABM.Authorization.Users;
using ABM.MultiTenancy;
using Microsoft.EntityFrameworkCore;

namespace ABM.EntityFrameworkCore;

public class ABMDbContext : AbpZeroDbContext<Tenant, Role, User, ABMDbContext>
{
    /* Define a DbSet for each entity of the application */

    public ABMDbContext(DbContextOptions<ABMDbContext> options)
        : base(options)
    {
    }
}
