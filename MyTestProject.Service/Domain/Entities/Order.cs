namespace MyTestProject.Service.Domain.Entities;

public class Order : EntityBase
{
    public Order()
    {
        Items = new List<OrderItem>();
    }
    public List<OrderItem> Items { get; set; }
}