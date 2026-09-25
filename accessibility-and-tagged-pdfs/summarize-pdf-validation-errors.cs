using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

class Program
{
    static void Main(string[] args)
    {
        // Expect the path to the XML validation report as the first argument
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <validationReport.xml>");
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

        // Locate elements that represent errors/violations.
        // Common names are Error, Violation, or Issue.
        var errorElements = doc.Descendants()
                               .Where(e => e.Name.LocalName.Equals("Error", StringComparison.OrdinalIgnoreCase) ||
                                           e.Name.LocalName.Equals("Violation", StringComparison.OrdinalIgnoreCase) ||
                                           e.Name.LocalName.Equals("Issue", StringComparison.OrdinalIgnoreCase));

        // Dictionaries to aggregate counts and keep a sample message per code
        var codeCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        var codeSamples = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        foreach (var err in errorElements)
        {
            // Try to obtain the error code from an attribute or a child element
            string code = (string)err.Attribute("code") ??
                          err.Element(err.Name.Namespace + "Code")?.Value ??
                          "UNKNOWN";

            // Try to obtain a descriptive message
            string message = (string)err.Attribute("message") ??
                             err.Element(err.Name.Namespace + "Message")?.Value ??
                             err.Value?.Trim();

            // Increment occurrence count
            if (codeCounts.ContainsKey(code))
                codeCounts[code]++;
            else
                codeCounts[code] = 1;

            // Store the first encountered message as a representative sample
            if (!codeSamples.ContainsKey(code) && !string.IsNullOrEmpty(message))
                codeSamples[code] = message;
        }

        // Output a summary sorted by descending frequency
        Console.WriteLine("Error Code Summary:");
        foreach (var kvp in codeCounts.OrderByDescending(k => k.Value))
        {
            Console.WriteLine($"{kvp.Key}: {kvp.Value} occurrence(s)");
            if (codeSamples.TryGetValue(kvp.Key, out string sample) && !string.IsNullOrEmpty(sample))
                Console.WriteLine($"  Sample message: {sample}");
        }

        // Highlight the top three most common violations
        Console.WriteLine("\nTop 3 common violations:");
        foreach (var kvp in codeCounts.OrderByDescending(k => k.Value).Take(3))
        {
            Console.WriteLine($"{kvp.Key} - {kvp.Value} times");
        }
    }
}