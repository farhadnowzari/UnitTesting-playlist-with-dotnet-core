using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyTestProject.Service.Domain;

namespace MyTestProject.Service.Tests.Common;

public class DbContextFactory
{
    private readonly IConfiguration _configuration;

    public DbContextFactory(IConfiguration configuration)
    {
        _configuration = configuration;

    }

    public void RunWithDbContext(Action<ApplicationDbContext> callback)
    {
        var dbContext = GetDbContext();
        callback(dbContext);
    }

    public T RunWithDbContext<T>(Func<ApplicationDbContext, T> callback)
    {
        var dbContext = GetDbContext();
        return callback(dbContext);
    }

    private ApplicationDbContext GetDbContext()
    {
        var dbContextOptionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseNpgsql(_configuration.GetConnectionString("main"));
        return new ApplicationDbContext(dbContextOptionsBuilder.Options);
    }
}