using System;
using System.Text.RegularExpressions;

class Program
{
    // Regular expression for MM/DD/YYYY format:
    // - Month: 01 to 12
    // - Day:   01 to 31 (basic validation, does not account for month length or leap years)
    // - Year:  four digits
    private static readonly Regex DateRegex = new Regex(
        @"^(0[1-9]|1[0-2])/(0[1-9]|[12][0-9]|3[01])/([0-9]{4})$",
        RegexOptions.Compiled);

    static void Main()
    {
        // Example date strings to test
        string[] testDates = {
            "02/28/2023", // valid
            "12/31/1999", // valid
            "00/10/2020", // invalid month
            "13/01/2020", // invalid month
            "04/31/2021", // invalid day (basic check allows 31 for any month)
            "6/15/2022",  // invalid format (single-digit month)
            "06/5/2022",  // invalid format (single-digit day)
            "06/15/22"    // invalid year length
        };

        foreach (var date in testDates)
        {
            bool isValid = IsValidDate(date);
            Console.WriteLine($"{date} => {(isValid ? "Valid" : "Invalid")}");
        }
    }

    // Returns true if the input matches MM/DD/YYYY format
    static bool IsValidDate(string date)
    {
        if (string.IsNullOrWhiteSpace(date))
            return false;

        return DateRegex.IsMatch(date);
    }
}