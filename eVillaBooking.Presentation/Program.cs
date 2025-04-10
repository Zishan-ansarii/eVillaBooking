using eVillaBooking.Application.Common.Interfaces;
using eVillaBooking.Domain.Entities;
using eVillaBooking.Infrastructure.Data;
using eVillaBooking.Infrastructure.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(opt =>
{
    //string connectionString = builder.Configuration.GetSection("Logging:LogLevel")["Default"];
    string connectionString = builder.Configuration.GetValue<string>("MyConnectionStrings:DefaultConnection")!;

    opt.UseSqlServer(connectionString);
});

builder.Services.AddIdentity<ApplicationUser,IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();


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

app.Run();
