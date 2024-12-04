using Microsoft.EntityFrameworkCore;
using My_Trip_Log.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register the DbContext with an in-memory database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("InMemoryTripLog"));

// Explicitly configure Kestrel to use port 5227
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(5227, listenOptions =>
    {
        listenOptions.UseHttps(); // HTTPS on port 5227
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Trips}/{action=Index}/{id?}");

app.Run();

