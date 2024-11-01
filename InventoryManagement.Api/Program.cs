using InventoryManagement.Data.Context;
using InventoryManagement.Server;
using InventoryManagement.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<InventoryDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("InventoryDatabase"),
        sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 3,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null);
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