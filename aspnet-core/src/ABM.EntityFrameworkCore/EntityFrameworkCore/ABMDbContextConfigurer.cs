using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace ABM.EntityFrameworkCore;

public static class ABMDbContextConfigurer
{
    public static void Configure(DbContextOptionsBuilder<ABMDbContext> builder, string connectionString)
    {
        builder.UseSqlServer(connectionString);
    }

    public static void Configure(DbContextOptionsBuilder<ABMDbContext> builder, DbConnection connection)
    {
        builder.UseSqlServer(connection);
    }
}
