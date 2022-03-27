using System.IO;
using System.Text.Json;
using MyTestProject.Service.Domain;

namespace MyTestProject.Service.Tests.Seeders;

public abstract class SeederBase<TData>
{

    public TData Data { get; set; }
    public SeederBase()
    {
        InitData();
    }

    private void InitData()
    {
        var seedName = this.GetType().Name;
        var json = File.ReadAllText($"./Data/{seedName}.json");
        this.Data = JsonSerializer.Deserialize<TData>(json);
    }

    public abstract void Execute(ApplicationDbContext dbContext);
}