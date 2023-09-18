namespace MinimalApi.Models;

public class Company
{
    public Guid Id { get; set; }
    public string? name { get; set; }
    public string? telephone { get; set; }
    public bool? active { get; set; }
    public DateTime? createdDate { get; set; }
    public string? createdBy { get; set; }
    public DateTime? lastModifiedDate { get; set; }
    public string? lastModifiedBy { get; set; }
}