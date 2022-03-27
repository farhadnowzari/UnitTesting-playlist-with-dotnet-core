namespace MyTestProject.Service.Domain.Entities;

public class Stock : EntityBase
{
    public int Amount { get; set; }
    public int SoldAmount { get; set; }

    public Guid ProductId { get; set; }

    public Product Product { get; set; }
}