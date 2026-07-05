using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

class Program
{
    static void Main()
    {
        // Path to the XML file that contains validation errors
        const string xmlPath = "errors.xml";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"File not found: {xmlPath}");
            return;
        }

        // Load the XML document
        XDocument xmlDoc = XDocument.Load(xmlPath);

        // The XML schema is assumed to have <Error> elements.
        // An error about missing alternative text typically contains the word "Alt"
        // (e.g., Message="Missing Alt text for image").
        // Each <Error> may reference one or more <Image> elements that have an Id attribute.
        List<string> imageIdsMissingAlt = xmlDoc
            .Descendants("Error")
            .Where(err =>
            {
                // Check the error message or inner text for the keyword "Alt"
                string message = (string)err.Attribute("Message") ?? err.Value;
                return !string.IsNullOrEmpty(message) && message.IndexOf("Alt", StringComparison.OrdinalIgnoreCase) >= 0;
            })
            .SelectMany(err => err.Descendants("Image"))
            .Select(img => (string)img.Attribute("Id"))
            .Where(id => !string.IsNullOrEmpty(id))
            .Distinct()
            .ToList();

        // Output the list of image IDs that are missing alternative text
        Console.WriteLine("Images missing alternative text:");
        foreach (string id in imageIdsMissingAlt)
        {
            Console.WriteLine(id);
        }
    }
}