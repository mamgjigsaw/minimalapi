using Microsoft.EntityFrameworkCore;
using MinimalApi.Models;

namespace MinimalApi.Data;

public class OfficeDb: DbContext
{
    public OfficeDb(DbContextOptions<OfficeDb> options) : base(options)
    {
        
    }
    public DbSet<Employee> Employees => Set<Employee>();
}