using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using demo_app_mozaik.Models;
using demo_app_mozaik.Data;
using Microsoft.EntityFrameworkCore;
using demo_app_mozaik.RequestHelper;
using demo_app_mozaik.Extensions;

namespace demo_app_mozaik.Controllers;

public class HomeController(DbStoreContext context) : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> GetAllMember()
    {
        var employees = await context.Employees.ToListAsync();

        return Ok(employees);
    }

    public async Task<IActionResult> GetEmployeePage([FromQuery] EmployeeParams employeeParams)
    {
        var query = context.Employees
        .Search(employeeParams.SearchEmployee)
        .AsQueryable();
        var employees = await PagedList<Employee>.
                        ToPagedList(query, employeeParams.PageNumber, employeeParams.PageSize);

        Response.AddPaginationHeader(employees.MetaData);
        return Ok(employees);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
