using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "optimized.pdf";

        // Verify that the source PDF exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document pdfDoc = new Document(inputPath))
        {
            // Linearize the document for faster web viewing.
            pdfDoc.Optimize();

            // Remove unused resources and merge identical ones.
            pdfDoc.OptimizeResources();

            // Save the optimized PDF. No SaveOptions needed for PDF output.
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"Optimized PDF saved to '{outputPath}'.");
    }
}