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

    // public async Task<IActionResult> GetAllMember()
    // {
    //     var employees = await context.Employees.ToListAsync();

    //     return Ok(employees);
    // }

    public async Task<IActionResult> GetEmployeePage([FromQuery] EmployeeParams employeeParams)
    {
        var query = context.Employees
        .Where(x => !x.IsDeleted)
        .Search(employeeParams.SearchEmployee)
        .AsQueryable();
        var employees = await PagedList<Employee>.
                        ToPagedList(query, employeeParams.PageNumber, employeeParams.PageSize);

        return Ok(new {data = employees, metadata = employees.MetaData});
        // return Ok(employees);
    }


    [HttpDelete("/api/employees/{id:int}")]
    public async Task<IActionResult> DeleteById(int id)
    {
        var employee = await context.Employees.FindAsync(id);

        if(employee == null ) return NotFound();

        employee.IsDeleted = true;

        var result = await context.SaveChangesAsync() >0;
        if(result) return Ok();

        return BadRequest("Problem deleting the Employee");
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
