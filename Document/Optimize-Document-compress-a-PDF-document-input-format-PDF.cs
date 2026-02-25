using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "compressed.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Enable resource optimization (removes unused resources, merges duplicates, etc.)
            doc.OptimizeResources();

            // Optionally apply a full optimization strategy (e.g., compress objects, subset fonts)
            OptimizationOptions opts = OptimizationOptions.All();
            doc.OptimizeResources(opts);

            // Enable linearization for faster web viewing (optional but reduces size)
            doc.Optimize();

            // Save the optimized PDF; this writes the compressed file
            doc.Save(outputPath);
        }

        Console.WriteLine($"Compressed PDF saved to '{outputPath}'.");
    }
}