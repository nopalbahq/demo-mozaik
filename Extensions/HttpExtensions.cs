using System;
using System.Text.Json;
using demo_app_mozaik.RequestHelper;

namespace demo_app_mozaik.Extensions;

public static class HttpExtensions
{
    public static void AddPaginationHeader(this HttpResponse response, PaginationMetaData metaData)
    {
        var option = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        response.Headers.Append("Pagination", JsonSerializer.Serialize(metaData, option));
    }
}
