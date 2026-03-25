using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

class Program
{
    static void Main()
    {
        const string inputXmlPath = "validation_report.xml"; // Path to the XML report
        const string outputPath = "missing_alt_image_ids.txt"; // Output file for IDs

        if (!File.Exists(inputXmlPath))
        {
            Console.Error.WriteLine($"Input XML not found: {inputXmlPath}");
            return;
        }

        try
        {
            // Load the XML document
            XDocument doc = XDocument.Load(inputXmlPath);

            // Query for <Error> elements whose <Message> contains both "alt" and "missing"
            // and collect the distinct Id attribute values.
            List<string> missingAltIds = (from e in doc.Descendants("Error")
                                          let message = (string)e.Element("Message")
                                          where !string.IsNullOrEmpty(message) &&
                                                message.IndexOf("alt", StringComparison.OrdinalIgnoreCase) >= 0 &&
                                                message.IndexOf("missing", StringComparison.OrdinalIgnoreCase) >= 0
                                          let id = (string)e.Attribute("Id")
                                          where !string.IsNullOrEmpty(id)
                                          select id)
                                         .Distinct()
                                         .ToList();

            // Write the IDs to the output file (one per line)
            File.WriteAllLines(outputPath, missingAltIds);

            Console.WriteLine($"Found {missingAltIds.Count} images with missing Alt text. IDs written to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing XML: {ex.Message}");
        }
    }
}
