using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

class ValidationReportParser
{
    // Entry point
    static void Main(string[] args)
    {
        // Expect the path to the XML validation report as the first argument
        if (args.Length == 0 || !File.Exists(args[0]))
        {
            Console.Error.WriteLine("Usage: ValidationReportParser <validation_report.xml>");
            return;
        }

        string xmlPath = args[0];

        try
        {
            // Load the XML document
            XDocument doc = XDocument.Load(xmlPath);

            // The structure of Aspose.Pdf validation reports typically contains
            // <Error> elements with attributes like Code and Message.
            // Adjust the element/attribute names if your report differs.
            var errorElements = doc.Descendants("Error");

            // Extract error information into a simple POCO
            var errors = errorElements
                .Select(e => new ValidationError
                {
                    Code = (string)e.Attribute("Code") ?? "UNKNOWN",
                    Message = (string)e.Attribute("Message") ?? e.Value.Trim()
                })
                .ToList();

            if (!errors.Any())
            {
                Console.WriteLine("No errors found in the validation report.");
                return;
            }

            // Group by error code to get counts
            var errorsByCode = errors
                .GroupBy(err => err.Code)
                .Select(g => new
                {
                    Code = g.Key,
                    Count = g.Count(),
                    SampleMessages = g
                        .Select(err => err.Message)
                        .Distinct()
                        .Take(3) // show up to 3 distinct messages per code
                })
                .OrderByDescending(g => g.Count);

            // Output summary
            Console.WriteLine("=== Validation Report Summary ===");
            Console.WriteLine($"Total errors: {errors.Count}");
            Console.WriteLine();

            foreach (var group in errorsByCode)
            {
                Console.WriteLine($"Error Code: {group.Code}");
                Console.WriteLine($"Occurrences: {group.Count}");
                Console.WriteLine("Sample messages:");
                foreach (var msg in group.SampleMessages)
                {
                    Console.WriteLine($"  - {msg}");
                }
                Console.WriteLine();
            }

            // Additionally, list the most common violation messages overall
            var commonMessages = errors
                .GroupBy(err => err.Message)
                .Select(g => new { Message = g.Key, Count = g.Count() })
                .OrderByDescending(g => g.Count)
                .Take(5);

            Console.WriteLine("=== Top 5 Common Violation Messages ===");
            foreach (var item in commonMessages)
            {
                Console.WriteLine($"{item.Count}× \"{item.Message}\"");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to process the validation report: {ex.Message}");
        }
    }

    // Simple container for error details
    private class ValidationError
    {
        public string Code { get; set; }
        public string Message { get; set; }
    }
}