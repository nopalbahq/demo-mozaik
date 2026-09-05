using System;

namespace demo_app_mozaik.Models;

public class Employee
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Departemen { get; set; }
    public required DateTime HireDate { get; set; }
    public required string JobTitle { get; set; }
}