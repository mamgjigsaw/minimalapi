using Microsoft.EntityFrameworkCore;
using MinimalApi.Data;
using MinimalApi.Models;
using Microsoft.AspNetCore.OpenApi;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("PostgreSQLConnection");
builder.Services.AddDbContext<OfficeDb>(options => options.UseNpgsql(connectionString));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => "Minimal API!");

app.MapPost("/employees", async (Employee e, OfficeDb db) =>
{
    db.Employees.Add(e);
    await db.SaveChangesAsync();

    return Results.Created($"/employee/{e.Id}", e);
});

app.MapGet("/employees/{id:int}", async (int id, OfficeDb db) => await db.Employees.FindAsync(id)
    is Employee e
    ? Results.Ok(e)
    : Results.NoContent());

app.MapGet("/employees", async (OfficeDb db) => await db.Employees.ToListAsync());

app.MapPut("/employees", async ( Employee e, OfficeDb db) =>
{
    var employee = await db.Employees.FindAsync(e.Id);

    if (employee is null) return Results.NoContent();

    employee.FirstName = e.FirstName;
    employee.LastName = e.LastName;
    employee.Branch = e.Branch;
    employee.Age = e.Age;

    await db.SaveChangesAsync();

    return Results.Ok(employee);
});

app.MapDelete("/employees/{id:int}", async (int id, OfficeDb db) =>
{
    var employee = await db.Employees.FindAsync(id);
    if (employee is null) return Results.NoContent();
    
    db.Employees.Remove(employee);
    await db.SaveChangesAsync();

    return Results.Ok("Record Deleted");
});

app.MapPost("/companies", async (Company e, OfficeDb db) =>
{
    e.Id = Guid.NewGuid();
    e.createdDate = DateTime.UtcNow;
    e.lastModifiedDate = DateTime.UtcNow;
    e.lastModifiedBy = e.createdBy;
    db.Companies.Add(e);
    await db.SaveChangesAsync();

    return Results.Created($"/companies/{e.Id}", e);
});

app.MapGet("/companies/{id:int}", async (int id, OfficeDb db) => await db.Companies.FindAsync(id)
    is Company e
    ? Results.Ok(e)
    : Results.NoContent());

app.Run();
