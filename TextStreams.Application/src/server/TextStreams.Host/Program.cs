using Serilog;
using TextStreams.AppServices;
using TextStreams.DataAccess;
using TextStreams.Host;
using TextStreams.Host.Hubs;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog(((context, services, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext()
    .WriteTo.Console()));

builder.Services.AddLogging(
        builder => { builder.AddConsole(); })
    .Configure<LoggerFilterOptions>(builder.Configuration.GetSection("Logging"));

builder.Services.AddControllers(o => o.Filters.Add<ServiceExceptionFilter>());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

var connectionString = builder.Configuration.GetSection("DbConnectionConfiguration:ConnectionString").Value;
if (connectionString == null)
{
    throw new Exception("Connection string not found");
}

builder.Services.AddHandlers()
    .AddValidators()
    .AddRepositories(connectionString);

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapHub<StreamHub>("/stream");

app.Run();