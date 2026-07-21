using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "portfolio.pdf";   // PDF Portfolio file
        const string outputPath = "flattened.pdf";   // Standard PDF output

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF Portfolio, flatten it, and save as a regular PDF
        using (Document doc = new Document(inputPath))
        {
            // Remove interactive elements (forms, annotations, etc.)
            doc.Flatten();

            // Save the flattened document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Flattened PDF saved to '{outputPath}'.");
    }
}