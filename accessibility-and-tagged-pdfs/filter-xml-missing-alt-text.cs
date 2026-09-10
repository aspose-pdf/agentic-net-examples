using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

class Program
{
    static void Main()
    {
        // Path to the XML file containing validation errors
        const string xmlPath = "validation_report.xml";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"Error: File not found – {xmlPath}");
            return;
        }

        try
        {
            // Load the XML document
            XDocument doc = XDocument.Load(xmlPath);

            // Query for error elements that indicate missing alternative text.
            // Assumes the XML structure contains <Error type="MissingAltText" imageId="...">...
            IEnumerable<string> missingAltImageIds = GetMissingAltImageIds(doc);

            // Output the list of affected image IDs
            Console.WriteLine("Images missing alternative text:");
            foreach (string id in missingAltImageIds)
            {
                Console.WriteLine($"- {id}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Exception while processing XML: {ex.Message}");
        }
    }

    /// <summary>
    /// Extracts image IDs from the XML document where the error type is "MissingAltText".
    /// </summary>
    /// <param name="doc">The loaded XDocument.</param>
    /// <returns>An enumerable of image ID strings.</returns>
    private static IEnumerable<string> GetMissingAltImageIds(XDocument doc)
    {
        // Adjust the element and attribute names if your XML uses different naming.
        foreach (var errorElement in doc.Descendants("Error"))
        {
            XAttribute typeAttr = errorElement.Attribute("type");
            XAttribute idAttr   = errorElement.Attribute("imageId");

            if (typeAttr != null && idAttr != null &&
                string.Equals(typeAttr.Value, "MissingAltText", StringComparison.OrdinalIgnoreCase))
            {
                yield return idAttr.Value;
            }
        }
    }
}