using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Expect the path to the XML validation report as the first argument
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: program <validationReport.xml>");
            return;
        }

        string xmlPath = args[0];
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"File not found: {xmlPath}");
            return;
        }

        XDocument doc;
        try
        {
            // Load the XML document
            doc = XDocument.Load(xmlPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to load XML: {ex.Message}");
            return;
        }

        // Gather all elements that represent an error/violation.
        // The schema may use <error> or <violation> tags.
        var errorElements = doc.Descendants()
                               .Where(e => e.Name.LocalName.Equals("error", StringComparison.OrdinalIgnoreCase) ||
                                           e.Name.LocalName.Equals("violation", StringComparison.OrdinalIgnoreCase))
                               .ToList();

        // Group errors by their code attribute (or "UNKNOWN" if missing)
        var groupedByCode = errorElements
            .GroupBy(e => (string)e.Attribute("code") ?? "UNKNOWN")
            .OrderBy(g => g.Key);

        Console.WriteLine("Error Code Summary:");
        foreach (var group in groupedByCode)
        {
            Console.WriteLine($"{group.Key}: {group.Count()} occurrence(s)");
        }

        // Determine the most common violation messages.
        // Use the 'message' attribute if present; otherwise fall back to element value.
        var messageGroups = errorElements
            .Select(e => (string)e.Attribute("message") ?? e.Value.Trim())
            .Where(m => !string.IsNullOrEmpty(m))
            .GroupBy(m => m)
            .OrderByDescending(g => g.Count())
            .Take(5); // show top 5 messages

        Console.WriteLine();
        Console.WriteLine("Top Violation Messages:");
        foreach (var msgGroup in messageGroups)
        {
            Console.WriteLine($"\"{msgGroup.Key}\" – {msgGroup.Count()} time(s)");
        }
    }
}