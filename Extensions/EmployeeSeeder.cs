using System;
using Bogus;
using Bogus.DataSets;
using demo_app_mozaik.Models;
using demo_app_mozaik.RequestHelper;
using SQLitePCL;

namespace demo_app_mozaik.Extensions;

public static class EmployeeSeeder
{
    public static List<Employee> CreateEmployees(int count = 100)
    {
        var departement = new List<string> {"HR", "Marketing", "Financel", "Sales"};
        var jobTitle = new List<string> {"Staff", "Director", "Supervisor"};
        var random = new Random();

        var faker = new Faker<Employee>()
        .RuleFor(e => e.FirstName, f => f.Name.FirstName())
        .RuleFor(e => e.LastName, f => f.Name.LastName())
        .RuleFor(e => e.Departement, f => f.PickRandom(departement))
        .RuleFor(e => e.HireDate, f => random.RandomDateTime())
        .RuleFor(e => e.Salary, f => f.Finance.Amount(5000000,50000000,2))
        .RuleFor(e => e.Email, (f,e) => f.Internet.Email(e.FirstName, e.LastName))
        .RuleFor(e => e.JobTitle, f=> f.PickRandom(jobTitle))
        .RuleFor(e => e.CreatedDate, f => DateTime.UtcNow)
        .RuleFor(e => e.IsDeleted, f => false);

        return faker.Generate(count);
    }
}
