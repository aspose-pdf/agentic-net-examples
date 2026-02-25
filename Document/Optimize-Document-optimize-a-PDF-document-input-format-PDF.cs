using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "optimized.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal.
        using (Document pdfDocument = new Document(inputPath))
        {
            // Linearize the document for faster web viewing.
            pdfDocument.Optimize();

            // Apply a full resource optimization strategy (removes unused resources,
            // merges duplicate streams, compresses objects, etc.).
            OptimizationOptions options = OptimizationOptions.All();
            pdfDocument.OptimizeResources(options);

            // Save the optimized PDF. The Save method writes a PDF regardless of the
            // file extension, so no SaveOptions are needed here.
            pdfDocument.Save(outputPath);
        }

        Console.WriteLine($"Optimized PDF saved to '{outputPath}'.");
    }
}