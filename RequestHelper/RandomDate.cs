using System;

namespace demo_app_mozaik.RequestHelper;

public static class RandomDate
{
    public static DateTime RandomDateTime(this Random random, int minYear = 2005, int maxYear = 2020)
    {
        var year = random.Next(minYear, maxYear + 1);
        var month = random.Next(1, 13);
        var noOfDaysInMonth = DateTime.DaysInMonth(year,month);
        var days = random.Next(1, noOfDaysInMonth +1);

        return new DateTime(year,month,days);
    }
}
