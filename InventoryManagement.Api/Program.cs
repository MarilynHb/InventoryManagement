using InventoryManagement.Data.Context;
using InventoryManagement.Server;
using InventoryManagement.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


builder.Host.UseSerilog((context, services, configuration) => configuration
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
    .WriteTo.File(
        path: "Logs/log-.txt",
        rollingInterval: RollingInterval.Day,
        outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff} {Level:u3}] {Message:lj}{NewLine}{Exception}")
);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<InventoryDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("InventoryDatabase"),
        sqlOptions =>
        {
            sqlOptions.CommandTimeout(30);
        }
    )
);

builder.Services.AddScoped<IInventoryEntities>(provider => provider.GetRequiredService<InventoryDbContext>());
builder.Services.AddScoped<IProductService, ProductService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging(options =>
{
    options.MessageTemplate = "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms";
});

app.UseCors(policy =>
    //need to check how to not have this as an issue
    //policy.WithOrigins("http://localhost:44354", "https://localhost:44354")
    policy.WithOrigins("http://localhost:7072", "https://localhost:7072")
     .AllowAnyMethod()
     .AllowAnyHeader()
     .WithExposedHeaders("Content-Disposition")
);

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();