using System;
using demo_app_mozaik.Extensions;
using demo_app_mozaik.Models;
using Microsoft.EntityFrameworkCore;

namespace demo_app_mozaik.Data;

public class DbInitializer
{

    public static async Task Init(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<DbStoreContext>()
        ?? throw new InvalidOperationException();
        
        await SeedData(context);

    }

    private static async Task SeedData(DbStoreContext context)
    {
        context.Database.Migrate();

        if(context.Employees.Any()) return;
        
        var employee = EmployeeSeeder.CreateEmployees(100);

        context.Employees.AddRange(employee);
        context.SaveChanges();
    }
}
