using DbUp;
using DbUp.Builder;
using DbUp.Engine;
using Microsoft.Extensions.Configuration;

IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

var result =
    DeployChanges.To
        .PostgresqlDatabase(configuration.GetSection("DbConnectionConfiguration:ConnectionString").Value)
        .WithScriptsFromFileSystem("./Scripts")
        .LogToConsole()
        .Build()
        .PerformUpgrade();