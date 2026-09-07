using System;
using demo_app_mozaik.Models;

namespace demo_app_mozaik.Extensions;

public static class EmployeeExtensions
{
    public static IQueryable<Employee> Search(this IQueryable<Employee> query, string? search)
    {
        query = query.Where(x => !x.IsDeleted);

        if(string.IsNullOrEmpty(search)) return query;

        var lowerCastSearch = search.Trim().ToLower();
        
        return query.Where(x => x.FirstName.ToLower().Contains(lowerCastSearch)||
                           x.LastName.ToLower().Contains(lowerCastSearch));

    }
}
