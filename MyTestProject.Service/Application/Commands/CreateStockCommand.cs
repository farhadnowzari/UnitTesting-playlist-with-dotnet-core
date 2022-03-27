using MediatR;
using MyTestProject.Service.Application.Exceptions;
using MyTestProject.Service.Domain;
using MyTestProject.Service.Domain.Entities;

namespace MyTestProject.Service.Application.Commands;

public class UpsertStockCommand : IRequest<Guid>
{
    public Guid? Id { get; set; }
    public Guid ProductId { get; set; }
    public int Amount { get; set; }
    public class UpsertStockCommandHandler : IRequestHandler<UpsertStockCommand, Guid>
    {
        private readonly ApplicationDbContext _dbContext;

        public UpsertStockCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<Guid> Handle(UpsertStockCommand request, CancellationToken cancellationToken)
        {
            var stock = _dbContext.Stocks.FirstOrDefault(x => x.Id == request.Id);
            if (stock == null)
            {
                stock = new Stock
                {
                    Id = Guid.NewGuid(),
                    ProductId = request.ProductId,
                    Amount = request.Amount
                };
                _dbContext.Stocks.Add(stock);
            }
            else
            {
                if(request.Amount < stock.SoldAmount) {
                    throw new ApplicationBadRequestException();
                }
                stock.Amount = request.Amount;
            }

            _dbContext.SaveChanges();

            return Task.FromResult(stock.Id);
        }
    }
}