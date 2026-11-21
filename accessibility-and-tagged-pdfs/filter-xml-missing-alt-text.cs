using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

class Program
{
    static void Main()
    {
        // Path to the XML validation report that contains image errors
        const string xmlPath = "validation_report.xml";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"File not found: {xmlPath}");
            return;
        }

        // Load the XML document (XDocument does not need a using block)
        XDocument doc = XDocument.Load(xmlPath);

        // The expected structure:
        // <Errors>
        //   <Error>
        //     <ImageID>img123</ImageID>
        //     <Message>Missing Alt text</Message>
        //   </Error>
        //   ...
        // </Errors>

        // Select all ImageID values where the Message indicates a missing Alt text.
        IEnumerable<string> imageIds = doc
            .Descendants("Error")
            .Where(err =>
            {
                var message = (string)err.Element("Message");
                // Look for the phrase "missing alt" (case‑insensitive)
                return !string.IsNullOrEmpty(message) &&
                       message.IndexOf("missing alt", StringComparison.OrdinalIgnoreCase) >= 0;
            })
            .Select(err => (string)err.Element("ImageID"))
            .Where(id => !string.IsNullOrEmpty(id));

        // Remove duplicates and materialize the list
        List<string> affectedIds = imageIds.Distinct().ToList();

        // Output the result
        Console.WriteLine("Images missing Alt text:");
        foreach (string id in affectedIds)
        {
            Console.WriteLine(id);
        }
    }
}