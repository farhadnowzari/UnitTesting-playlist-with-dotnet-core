using System;
using System.Collections.Generic;
using MyTestProject.Service.Domain;
using MyTestProject.Service.Domain.Entities;

namespace MyTestProject.Service.Tests.Seeders;

public class ProductsSeeder : SeederBase<List<Product>>
{
    public override void Execute(ApplicationDbContext dbContext)
    {
        Data.ForEach(x => x.Id = Guid.NewGuid());

        dbContext.Products.AddRange(Data);
        dbContext.SaveChanges();
    }
}