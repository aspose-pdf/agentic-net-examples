using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "optimized.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF, optimize it for web viewing, and save the result.
        using (Document pdfDocument = new Document(inputPath))
        {
            // Linearize the document (fast first-page load, incremental rendering).
            pdfDocument.Optimize();

            // Persist the optimized document.
            pdfDocument.Save(outputPath);
        }

        Console.WriteLine($"Optimized PDF saved to '{outputPath}'.");
    }
}