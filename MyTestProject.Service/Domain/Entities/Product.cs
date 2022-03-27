namespace MyTestProject.Service.Domain.Entities;
public class Product : EntityBase
{
    public Product()
    {
        Stocks = new List<Stock>();
    }
    public string Name { get; set; }
    public decimal Price { get; set; }


    public List<Stock> Stocks { get; private set; }
}