using System;
using MyTestProject.Service.Domain;
using MyTestProject.Service.Domain.Entities;

namespace MyTestProject.Service.Tests.Seeders;

public class ProductWithStockSeeder : SeederBase<Product>
{
    public override void Execute(ApplicationDbContext dbContext)
    {
        Data.Id = Guid.NewGuid();
        Data.Stocks.Add(new Stock {
            Id = Guid.NewGuid(),
            Amount = 1000,
            SoldAmount = 400
        });
        dbContext.Products.Add(Data);
        dbContext.SaveChanges();
    }
}