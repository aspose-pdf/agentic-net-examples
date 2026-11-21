using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_xmp.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Add a custom metadata entry. Use a prefixed key to represent a custom namespace.
            const string customKey = "my:MyCustomProperty"; // "my" is an arbitrary prefix for the custom namespace
            const string propertyValue = "CustomValue";

            // Document.Metadata is a read‑only dictionary; we modify it via its indexer.
            doc.Metadata[customKey] = propertyValue;

            // Save the document; this preserves all existing metadata and adds the new entry.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with custom metadata to '{outputPath}'.");
    }
}
