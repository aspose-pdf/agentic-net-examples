using System;
using System.Text.RegularExpressions;

class Program
{
    // Regular expression for MM/DD/YYYY format:
    // - Month: 01 to 12
    // - Day:   01 to 31 (basic validation, not accounting for month length or leap years)
    // - Year:  four digits
    private static readonly Regex DateRegex = new Regex(
        @"^(0[1-9]|1[0-2])/(0[1-9]|[12][0-9]|3[01])/(\d{4})$",
        RegexOptions.Compiled);

    // Returns true if the input matches the MM/DD/YYYY pattern
    static bool ValidateDate(string date)
    {
        if (string.IsNullOrWhiteSpace(date))
            return false;

        return DateRegex.IsMatch(date);
    }

    static void Main()
    {
        // Example inputs
        string[] testDates = {
            "02/28/2023", // valid
            "12/31/1999", // valid
            "00/10/2020", // invalid month
            "13/01/2020", // invalid month
            "04/31/2021", // invalid day (basic pattern still accepts, deeper validation needed)
            "5/5/2022",   // invalid format (single-digit month/day)
            "05/05/22",   // invalid year length
            ""            // invalid (empty)
        };

        foreach (var date in testDates)
        {
            bool isValid = ValidateDate(date);
            Console.WriteLine($"{date,-12} => {(isValid ? "Valid" : "Invalid")}");
        }
    }
}