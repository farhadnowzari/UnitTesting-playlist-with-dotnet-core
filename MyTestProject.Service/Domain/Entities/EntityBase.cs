namespace MyTestProject.Service.Domain.Entities;

public abstract class EntityBase
{
    public Guid Id { get; set; }

    public DateTime ModifiedOn { get; set; }
    public DateTime CreatedOn { get; set; }
}