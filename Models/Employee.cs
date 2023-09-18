using System.Runtime.InteropServices.JavaScript;

namespace MinimalApi.Models;

public class Employee
{
    public Int64 Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Branch { get; set; }
    public int Age { get; set; }
}