using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "portfolio.pdf";   // PDF Portfolio input
        const string outputPath = "flattened.pdf";   // Standard PDF output

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF Portfolio, then flatten it to remove interactive collection features
        using (Document doc = new Document(inputPath))
        {
            // Flatten removes form fields and interactive elements; for a portfolio this also strips the collection UI
            doc.Flatten();

            // Save as a regular PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Portfolio flattened and saved to '{outputPath}'.");
    }
}