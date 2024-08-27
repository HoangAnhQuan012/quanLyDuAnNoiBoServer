using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace quanLyDuAnNoiBo.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class quanLyDuAnNoiBoDbContextFactory : IDesignTimeDbContextFactory<quanLyDuAnNoiBoDbContext>
{
    public quanLyDuAnNoiBoDbContext CreateDbContext(string[] args)
    {
        quanLyDuAnNoiBoEfCoreEntityExtensionMappings.Configure();

        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<quanLyDuAnNoiBoDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));

        return new quanLyDuAnNoiBoDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../quanLyDuAnNoiBo.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
