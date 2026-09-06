using System;
using demo_app_mozaik.Models;
using Microsoft.EntityFrameworkCore;

namespace demo_app_mozaik.Data;

public class DbStoreContext(DbContextOptions<DbStoreContext> options) : DbContext(options)
{
    public required DbSet<Employee> Employees { get; set; } 
}