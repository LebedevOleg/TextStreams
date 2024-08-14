using Commentator.Application;
using Microsoft.Extensions.Configuration;

var builder = new ConfigurationBuilder()
    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

UserInterface userInterface = new UserInterface(builder.Build());

userInterface.MainMenu().Wait();