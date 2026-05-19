using Microsoft.EntityFrameworkCore;
using CloudNativeInventory.Api.Data;
using CloudNativeInventory.Api.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi(); // .NET 9 OpenAPI

// اتصال به دیتابیس با استفاده از پوشه دیتای خودت
builder.Services.AddDbContext<InventoryDbContext>(options =>
    options.UseInMemoryDatabase("InventoryDb"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// سید کردن داده‌ها
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<InventoryDbContext>();

    if (!db.Products.Any())
    {
        db.Products.Add(new Product { Id = 1, Name = "Laptop", Price = 9999, StockQuantity = 10 });
        db.SaveChanges();
    }
}

app.Run();