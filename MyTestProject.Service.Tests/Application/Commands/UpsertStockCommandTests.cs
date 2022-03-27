using System.Linq;
using System.Threading;
using FluentAssertions;
using MyTestProject.Service.Application.Commands;
using MyTestProject.Service.Application.Exceptions;
using MyTestProject.Service.Tests.Common;
using MyTestProject.Service.Tests.Seeders;
using Xunit;

namespace MyTestProject.Service.Tests.Application.Commands;

public class UpsertStockCommandTests : TestBase
{
    public UpsertStockCommandTests()
    {
        _dbContextFactory.RunWithDbContext(context =>
        {
            new ProductsSeeder().Execute(context);
            new ProductWithStockSeeder().Execute(context);
        });
    }
    [Fact]
    public void ShouldCreateStock()
    {
        var dto = GetDto<UpsertStockCommand>("CreateStockDto");
        _dbContextFactory.RunWithDbContext(context =>
        {
            dto.ProductId = context.Products.First().Id;
        });
        var stockId = _dbContextFactory.RunWithDbContext(context =>
        {
            var sut = new UpsertStockCommand.UpsertStockCommandHandler(context);
            return sut.Handle(dto, CancellationToken.None).Result;
        });

        _dbContextFactory.RunWithDbContext(context =>
        {
            var stock = context.Stocks.First(x => x.Id == stockId);
            stock.ProductId.Should().Be(dto.ProductId);
            stock.Amount.Should().Be(dto.Amount);
        });
    }

    [Fact]
    public void ShouldUpdateStock()
    {
        var stockUnderTest = _dbContextFactory.RunWithDbContext(context =>
        {
            return context.Stocks.First();
        });
        var dto = new UpsertStockCommand
        {
            Id = stockUnderTest.Id,
            ProductId = stockUnderTest.ProductId,
            Amount = 500
        };
        _dbContextFactory.RunWithDbContext(context =>
        {
            var sut = new UpsertStockCommand.UpsertStockCommandHandler(context);
            sut.Handle(dto, CancellationToken.None).Wait();
        });
        _dbContextFactory.RunWithDbContext(context =>
        {
            var _stock = context.Stocks.First(x => x.Id == stockUnderTest.Id);
            _stock.Amount.Should().Be(dto.Amount);
        });
    }

    [Fact]
    public void ShouldThrowBadRequestOnChangingTheStockAmountToBeLowerThanTheSoldAmount()
    {
        var stockUnderTest = _dbContextFactory.RunWithDbContext(context =>
        {
            return context.Stocks.First();
        });
        var dto = new UpsertStockCommand
        {
            Id = stockUnderTest.Id,
            ProductId = stockUnderTest.ProductId,
            Amount = 300
        };
        var act = () => _dbContextFactory.RunWithDbContext(context =>
        {
            var sut = new UpsertStockCommand.UpsertStockCommandHandler(context);
            sut.Handle(dto, CancellationToken.None).Wait();
        });

        act.Should().Throw<ApplicationBadRequestException>();
    }
}