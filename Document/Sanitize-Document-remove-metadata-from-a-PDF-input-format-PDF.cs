using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "sanitized.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF and ensure deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Remove all metadata (title, author, custom entries, etc.)
            doc.RemoveMetadata();

            // Save the cleaned PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Metadata removed. Saved sanitized PDF to '{outputPath}'.");
    }
}