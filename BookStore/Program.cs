using Bookstore.Controllers;
using Bookstore.Data;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register the SearchController
builder.Services.AddScoped<SearchController>();

// Configure database (assuming you have a connection string named "BookstoreConnection")
builder.Services.AddDbContext<BookstoreDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BookstoreConnection")));

// Register Repositories
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();

// Register DbInitializer
builder.Services.AddTransient<DbInitializer>(); // Register DbInitializer as transient

var app = builder.Build();

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

// Seed database on application startup
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbInitializer = services.GetRequiredService<DbInitializer>();
    dbInitializer.Initialize();
}

app.Run();