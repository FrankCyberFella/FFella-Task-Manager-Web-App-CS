
using FFella_TaskManager_CS.Models;
using System.Configuration;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<TasksDbContext> (option =>
    option.UseSqlServer(builder.Configuration.GetConnectionString("TasksDb")));

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        });
});

// Add services to the container.
//builder.Services.AddControllersWithViews();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseCors();

app.UseAuthorization();
/*
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
*/

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Tasks}/{action=Index}");

app.Run();
