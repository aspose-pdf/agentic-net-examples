using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_metadata.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Update standard document information
            doc.Info.Title = "Report with Custom XML Metadata";
            doc.Info.Author = "Acme Corp";

            // Add custom XML metadata (key/value pair) to the document info dictionary
            const string xmlKey = "CustomXml";
            const string xmlValue = @"<metadata><processId>12345</processId><timestamp>2024-09-28T12:34:56Z</timestamp></metadata>";

            // Store custom metadata using the DocumentInfo indexer (supported API)
            doc.Info[xmlKey] = xmlValue;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with custom XML metadata to '{outputPath}'.");
    }
}
