using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Build custom XML metadata
            string customXml = $"<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n" +
                "<customMetadata>\n" +
                "    <processId>12345</processId>\n" +
                $"    <timestamp>{DateTime.UtcNow:O}</timestamp>\n" +
                "    <notes>Downstream processing information</notes>\n" +
                "</customMetadata>";

            // Store the raw XML in the document's custom metadata dictionary using the indexer
            doc.Info["CustomXml"] = customXml;

            // Optionally add simple key/value pairs to the custom metadata dictionary
            doc.Info["ProcessId"] = "12345";
            doc.Info["Timestamp"] = DateTime.UtcNow.ToString("o");

            // Save while the Document object is still alive
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with custom XML metadata to '{outputPath}'.");
    }
}
