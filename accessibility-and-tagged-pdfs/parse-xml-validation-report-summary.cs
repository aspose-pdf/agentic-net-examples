using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // Path to the XML validation report
        const string xmlPath = "validation_report.xml";

        // Verify that the file exists before attempting to load it
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"File not found: {xmlPath}");
            return;
        }

        // Load the XML document into memory
        XDocument doc = XDocument.Load(xmlPath);

        // --------------------------------------------------------------------
        // Extract error codes – assumes the report uses <ErrorCode> elements.
        // Trim whitespace and ignore empty entries.
        // --------------------------------------------------------------------
        List<string> errorCodes = doc.Descendants("ErrorCode")
                                     .Select(e => e.Value.Trim())
                                     .Where(v => !string.IsNullOrEmpty(v))
                                     .ToList();

        // --------------------------------------------------------------------
        // Extract violation messages – assumes the report uses <Violation> elements.
        // --------------------------------------------------------------------
        List<string> violations = doc.Descendants("Violation")
                                     .Select(e => e.Value.Trim())
                                     .Where(v => !string.IsNullOrEmpty(v))
                                     .ToList();

        // --------------------------------------------------------------------
        // Summarize error codes: count occurrences of each distinct code.
        // --------------------------------------------------------------------
        var errorCodeSummary = errorCodes
                               .GroupBy(code => code)
                               .Select(g => new { Code = g.Key, Count = g.Count() })
                               .OrderByDescending(x => x.Count);

        // --------------------------------------------------------------------
        // Summarize violations: count occurrences of each distinct message.
        // --------------------------------------------------------------------
        var violationSummary = violations
                               .GroupBy(v => v)
                               .Select(g => new { Text = g.Key, Count = g.Count() })
                               .OrderByDescending(x => x.Count);

        // Output the results
        Console.WriteLine("Error Code Summary:");
        foreach (var item in errorCodeSummary)
        {
            Console.WriteLine($"  {item.Code}: {item.Count}");
        }

        Console.WriteLine();
        Console.WriteLine("Common Violation Summary:");
        foreach (var item in violationSummary)
        {
            Console.WriteLine($"  \"{item.Text}\": {item.Count}");
        }
    }
}