using System;
using Microsoft.EntityFrameworkCore;

namespace demo_app_mozaik.Models;

public class Employee
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Departement { get; set; }
    public required DateTime HireDate { get; set; }
    
    [Precision(18, 2)]
    public decimal Salary { get; set; }
    public string? Email { get; set; }
    public required string JobTitle { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool IsDeleted { get; set; }
    public string? Gender { get; set; }
}