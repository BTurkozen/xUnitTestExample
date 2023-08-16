using Microsoft.EntityFrameworkCore;
using xUnitTestExample.Web.Models;
using xUnitTestExample.Web.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

var app = builder.Build();

using var scope = app.Services.CreateScope();

var providerService = scope.ServiceProvider;

var dataContext = providerService.GetRequiredService<DataContext>();

dataContext.Database.Migrate();

if (dataContext.Products.Any() is false)
{
    var products = new List<Product>
    {
        new Product{ Name = "Book", Quantity = 1, Price = 10},
        new Product{ Name = "Computer", Quantity = 12, Price = 110},
        new Product{ Name = "Mouse", Quantity = 14, Price = 50},
    };

    await dataContext.Products.AddRangeAsync(products);

    await dataContext.SaveChangesAsync();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
