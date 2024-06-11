using FFella_TaskManager_CS.Models;       // Provide access to Application classes and other data related components
using Microsoft.EntityFrameworkCore;      // Provide access to the EntityFramework ORM

// Instantiate and initialize builder for web applications and services.
var builder = WebApplication.CreateBuilder(args);

// Add services to the WebApplication container

// Add DbContext for server and database used in the app
// Server Connection String is stored in and obtained from appsettings.json file
builder.Services.AddDbContext<TasksDbContext> (option =>
    option.UseSqlServer(builder.Configuration.GetConnectionString("TasksDb")));

// Enable CORS for the app
// All requests from anywhere for any method for testing purposes
//     we would normally restrict which domains/methods we would accepts requests from
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        });
});

// Register everything that is needed for Web API Development.
//    This includes support for Controllers, Model Binding, API Explorer, Authorization, CORS et al
builder.Services.AddControllers();

// Run the WebApplication object builder which uses the options specifed above
var app = builder.Build();

// Authorize app to access secure resources
// app.UseAuthorization();  // Authorization is not used in this app

// Have URL paths defined for controller methods mapped in the server
app.MapControllers();

// Start the server side processing
app.Run();
