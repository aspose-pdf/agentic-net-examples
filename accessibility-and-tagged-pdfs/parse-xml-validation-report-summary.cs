using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

class Program
{
    static void Main()
    {
        // Path to the XML validation report
        const string xmlPath = "validation_report.xml";

        // Path where the summary will be saved
        const string summaryPath = "error_summary.txt";

        // Verify that the input file exists
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"File not found: {xmlPath}");
            return;
        }

        // Load the XML document
        XDocument xmlDoc = XDocument.Load(xmlPath);

        // Extract error codes.
        // This works for two common patterns:
        //   <Error Code="E001" ... />
        //   <Error><Code>E001</Code></Error>
        IEnumerable<string> codes = xmlDoc
            .Descendants("Error")
            .Select(e => (string)e.Attribute("Code") ?? (string)e.Element("Code"))
            .Where(c => !string.IsNullOrWhiteSpace(c));

        // Count how many times each code appears
        var frequency = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        foreach (string code in codes)
        {
            if (frequency.ContainsKey(code))
                frequency[code]++;
            else
                frequency[code] = 1;
        }

        // Prepare a human‑readable summary
        var summaryLines = new List<string>
        {
            "Error Code Summary:"
        };
        foreach (var kvp in frequency.OrderByDescending(k => k.Value))
        {
            summaryLines.Add($"{kvp.Key}: {kvp.Value}");
        }

        // Write the summary to the console
        foreach (string line in summaryLines)
        {
            Console.WriteLine(line);
        }

        // Persist the summary to a text file
        File.WriteAllLines(summaryPath, summaryLines);
        Console.WriteLine($"Summary saved to {summaryPath}");
    }
}