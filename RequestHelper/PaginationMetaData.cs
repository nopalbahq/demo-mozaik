using System;

namespace demo_app_mozaik.RequestHelper;

public class PaginationMetaData
{
    public int TotalCount { get; set; }
    public int PageSize { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
}
