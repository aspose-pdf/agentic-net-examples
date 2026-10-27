using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

class Program
{
    static void Main()
    {
        const string xmlPath = "validation_report.xml";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"File not found: {xmlPath}");
            return;
        }

        // Load the XML validation report
        XDocument doc = XDocument.Load(xmlPath);

        // Find all <Image> elements that have an Id attribute but lack a non‑empty Alt attribute
        List<string> missingAltIds = doc.Descendants("Image")
            .Where(img =>
                // Image must have an Id attribute
                !string.IsNullOrWhiteSpace((string)img.Attribute("Id")) &&
                // Alt attribute is missing or empty/whitespace
                string.IsNullOrWhiteSpace((string)img.Attribute("Alt")))
            .Select(img => (string)img.Attribute("Id"))
            .Distinct()
            .ToList();

        Console.WriteLine("Images missing Alt text:");
        foreach (string id in missingAltIds)
        {
            Console.WriteLine(id);
        }
    }
}