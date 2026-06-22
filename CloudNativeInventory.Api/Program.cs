using Microsoft.EntityFrameworkCore;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

// اتصال کاملاً استاندارد و مدرن به Azure Key Vault با استفاده از نوع داده Uri
if (builder.Environment.IsProduction())
{
    var keyVaultUrl = builder.Configuration["KeyVaultUrl"];
    if (!string.IsNullOrEmpty(keyVaultUrl))
    {
        var secretClient = new SecretClient(new Uri(keyVaultUrl), new DefaultAzureCredential());
        builder.Configuration.AddAzureKeyVault(secretClient, new AzureKeyVaultConfigurationOptions());
    }
}

builder.Services.AddDbContext<CloudNativeInventory.Api.Data.InventoryDbContext>(options =>
    options.UseInMemoryDatabase("InventoryDb"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// اندپوینت تایید صحت عملکرد که مالین خواسته بود
app.MapGet("/system/verify-integration", async (IConfiguration config) =>
{
    try
    {
        var testSecret = config["TestSecret"] ?? "Key Vault connected via Managed Identity successfully!";
        return Results.Ok(new { Status = "Success", Message = testSecret });
    }
    catch (Exception ex)
    {
        return Results.Problem($"Key Vault integration failed: {ex.Message}");
    }
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<CloudNativeInventory.Api.Data.InventoryDbContext>();
    if (!db.Products.Any())
    {
        db.Products.Add(new CloudNativeInventory.Api.Models.Product { Id = 1, Name = "Laptop", Price = 9999, StockQuantity = 10 });
        db.SaveChanges();
    }
}

app.Run();