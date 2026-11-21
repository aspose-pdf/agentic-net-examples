using System;
using System.IO;
using System.Xml.Linq;
using System.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "metadata.xml";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Retrieve the embedded XMP metadata via the Metadata dictionary
                var metadata = doc.Metadata;

                if (metadata == null || metadata.Count == 0)
                {
                    Console.WriteLine("No XMP metadata found in the PDF.");
                    return;
                }

                // Build XML representation of the metadata dictionary
                var entries = metadata.Select(kvp =>
                    new XElement("Entry",
                        new XAttribute("Key", kvp.Key),
                        // XmpValue must be converted to a string before being placed in CDATA
                        new XCData(kvp.Value?.ToString() ?? string.Empty)));

                var xmpXml = new XDocument(
                    new XElement("XmpMetadata", entries)
                );

                // Save the XML to the output file
                xmpXml.Save(outputPath);
                Console.WriteLine($"XMP metadata extracted to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
