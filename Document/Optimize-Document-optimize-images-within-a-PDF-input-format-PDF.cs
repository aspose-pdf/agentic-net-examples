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

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Optimize resources (including images) in the document.
            // This removes unused resources and merges identical ones.
            // For more aggressive image compression you could configure
            // an OptimizationOptions instance, but the default call is sufficient
            // for a general optimization pass.
            doc.OptimizeResources();

            // Optionally linearize the PDF for faster web viewing.
            doc.Optimize();

            // Save the optimized PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Optimized PDF saved to '{outputPath}'.");
    }
}