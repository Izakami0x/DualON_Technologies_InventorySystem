using DualON_Technologies_InventorySystem.Data;
using Microsoft.EntityFrameworkCore;
//Repositories
using DualON_Technologies_InventorySystem.Repositories;
using DualON_Technologies_InventorySystem.Repositories.Interfaces;
//Services
using DualON_Technologies_InventorySystem.Services;
using DualON_Technologies_InventorySystem.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args); // <-- this was missing

// Configure SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=inventory.db"));

//Register repositories and services
builder.Services.AddScoped<IProductRepository, ProductRepository>();

//Registering InventoryService with its interface IInventoryService for dependency injection
builder.Services.AddScoped<IInventoryService, InventoryService>();

// Add controllers with views (MVC)
builder.Services.AddControllersWithViews();

var app = builder.Build();

//Enable WAL Mode (Write-Ahead Logging)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.ExecuteSqlRaw("PRAGMA journal_mode=WAL;");
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // standard for serving wwwroot content

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();