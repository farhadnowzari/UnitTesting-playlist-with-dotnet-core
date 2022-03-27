using MediatR;
using MyTestProject.Service.Domain;
using MyTestProject.Service.Domain.Entities;

namespace MyTestProject.Service.Application.Commands;

public class CreateProductCommand : IRequest<Guid>
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Amount { get; set; }
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMediator _mediator;

        public CreateProductCommandHandler(ApplicationDbContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }
        public Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Price = request.Price
            };

            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                _dbContext.Products.Add(product);
                _dbContext.SaveChanges();

                _mediator.Send(new UpsertStockCommand
                {
                    ProductId = product.Id,
                    Amount = request.Amount
                }).Wait();

                transaction.Commit();
            }

            return Task.FromResult(product.Id);
        }
    }
}