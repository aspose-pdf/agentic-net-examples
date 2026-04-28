using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

class Program
{
    static void Main()
    {
        // Path to the XML file that contains validation errors.
        const string xmlPath = "validation_errors.xml";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"File not found: {xmlPath}");
            return;
        }

        try
        {
            // Load the XML document.
            XDocument doc = XDocument.Load(xmlPath);

            // Example XML structure assumed:
            // <Errors>
            //   <Error type="MissingAltText" imageId="img001" message="Alt text is missing." />
            //   <Error type="OtherError" ... />
            // </Errors>

            // Filter all <Error> elements where the type indicates a missing alt text.
            IEnumerable<XElement> missingAltErrors = doc
                .Descendants("Error")
                .Where(e => (string)e.Attribute("type") == "MissingAltText");

            // Extract the image IDs from the filtered errors.
            List<string> imageIds = missingAltErrors
                .Select(e => (string)e.Attribute("imageId"))
                .Where(id => !string.IsNullOrEmpty(id))
                .Distinct()
                .ToList();

            // Output the result.
            if (imageIds.Count == 0)
            {
                Console.WriteLine("No missing Alt text errors were found.");
            }
            else
            {
                Console.WriteLine("Images with missing Alt text:");
                foreach (string id in imageIds)
                {
                    Console.WriteLine($"- {id}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing XML: {ex.Message}");
        }
    }
}