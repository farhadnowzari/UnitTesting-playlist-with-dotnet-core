using Microsoft.Extensions.Configuration;

namespace MyTestProject.Service.Tests.Common;

public class ConfigurationFactory {
    public static IConfiguration GetConfiguration() {
        var builder = new ConfigurationBuilder();
        builder.AddJsonFile("./appsettings.json");
        return builder.Build();
    }
}