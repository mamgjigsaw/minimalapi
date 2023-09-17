using Microsoft.EntityFrameworkCore;
using MinimalApi.Data;
using MinimalApi.Models;

var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("PostgreSQLConnection");
builder.Services.AddDbContext<OfficeDb>(options => options.UseNpgsql(connectionString));

var app = builder.Build();

app.MapGet("/", () => "Minimal API!");

app.MapPost("/employees/", async (Employee e, OfficeDb db) =>
{
    db.Employees.Add(e);
    await db.SaveChangesAsync();

    return Results.Created($"/employee/{e.Id}", e);
});

app.MapGet("/employees/{id:int}", async (int id, OfficeDb db) =>
{
    return await db.Employees.FindAsync(id)
        is Employee e
        ? Results.Ok(e)
        : Results.NotFound();
});

app.MapGet("/employees", async (OfficeDb db) =>
{
    return await db.Employees.ToListAsync();
    ;
});

app.Run();
