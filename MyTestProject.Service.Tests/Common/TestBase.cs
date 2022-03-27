using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace MyTestProject.Service.Tests.Common;

[Collection("my_serial_tests")]
public abstract class TestBase
{
    public readonly DbContextFactory _dbContextFactory;

    public TestBase()
    {
        _dbContextFactory = new DbContextFactory(ConfigurationFactory.GetConfiguration());
        MigrateDatabase();
        CleanDatabase();
    }

    private void CleanDatabase()
    {
        _dbContextFactory.RunWithDbContext(context =>
        {
            context.RemoveRange(context.Stocks);
            context.RemoveRange(context.Products);
            context.SaveChanges();
        });
    }

    public T GetDto<T>(string name)
    {
        var directory = this.GetType().Name;
        var jsonDto = File.ReadAllText($"./Data/{directory}/{name}.json");
        return JsonSerializer.Deserialize<T>(jsonDto);
    }

    private void MigrateDatabase()
    {
        _dbContextFactory.RunWithDbContext(context =>
        {
            if (!context.Database.CanConnect())
            {
                context.Database.Migrate();
            }
            else
            {
                if (context.Database.GetAppliedMigrations().Count() < context.Database.GetMigrations().Count())
                {
                    context.Database.Migrate();
                }
            }
        });
    }
}