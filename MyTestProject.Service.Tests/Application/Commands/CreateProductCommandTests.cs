using System;
using System.Linq;
using System.Threading;
using FluentAssertions;
using MediatR;
using Moq;
using MyTestProject.Service.Application.Commands;
using MyTestProject.Service.Tests.Common;
using Xunit;

namespace MyTestProject.Service.Tests.Application.Commands;

public class CreateProductCommandTests : TestBase
{

    [Fact]
    public void ShouldCreateAProductFromTheGivenDTO()
    {
        var dto = GetDto<CreateProductCommand>("CreateProductDto");
        var mediatorMock = MediatorFactory.GetMediatorMock();
        var productId = ExecuteSUT(dto, mediatorMock.Object);
        _dbContextFactory.RunWithDbContext(dbContext =>
        {
            var product = dbContext.Products.First(x => x.Id == productId);
            product.Name.Should().Be(dto.Name);
            product.Price.Should().Be(dto.Price);
        });
    }

    [Fact]
    public void ShouldCallTheUpsertStockHandlerOnce()
    {
        var dto = GetDto<CreateProductCommand>("CreateProductWithStockDto");
        var mediatorMock = MediatorFactory.GetMediatorMock();
        var productId = ExecuteSUT(dto, mediatorMock.Object);
        _dbContextFactory.RunWithDbContext(dbContext =>
        {
            var product = dbContext.Products.First(x => x.Id == productId);
        });

        mediatorMock.Verify(x => x.Send(It.IsAny<UpsertStockCommand>(), CancellationToken.None), Times.Once());

    }

    private Guid ExecuteSUT(CreateProductCommand request, IMediator mediator)
    {
        return _dbContextFactory.RunWithDbContext(context =>
        {
            var sut = new CreateProductCommand.CreateProductCommandHandler(context, mediator);
            return sut.Handle(request, CancellationToken.None).Result;
        });
    }
}