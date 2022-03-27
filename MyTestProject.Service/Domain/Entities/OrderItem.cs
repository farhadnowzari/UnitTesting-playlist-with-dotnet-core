namespace MyTestProject.Service.Domain.Entities;

public class OrderItem : EntityBase
{
    public Guid OrderId { get; set; }

    public Order Order { get; set; }
}