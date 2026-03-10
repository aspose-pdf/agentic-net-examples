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

        // Load the PDF inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Set standard document info properties
            doc.Info.Title = "Sample PDF Title";
            doc.Info.Author = "John Doe";

            // Add custom XMP metadata entries using the Metadata property
            // XMP keys must be valid XML names and follow XMP specification
            // Use XmpValue wrapper for non-string values
            // Ensure keys are valid XML names (no spaces, special chars, etc.)
            // Use qualified namespace prefix (e.g., "xmp:CustomKey1") to avoid invalid key errors
            doc.Metadata.Add("xmp:CustomKey1", new XmpValue("CustomValue1"));
            doc.Metadata.Add("xmp:CustomKey2", new XmpValue(12345));

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Metadata updated and saved to '{outputPath}'.");
    }
}