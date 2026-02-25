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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Optional: merge identical resource streams when pages are added
            doc.OptimizeSize = true;

            // Configure optimization options:
            // - compress objects,
            // - remove unused objects and streams,
            // - subset fonts (keeps only used glyphs),
            // - other default optimizations can be added as needed.
            OptimizationOptions opt = new OptimizationOptions
            {
                CompressObjects      = true,
                RemoveUnusedObjects  = true,
                RemoveUnusedStreams  = true,
                SubsetFonts          = true
                // ImageCompressionOptions can be set here if specific image settings are required
            };

            // Apply resource optimizations (removes unused resources, compresses objects, etc.)
            doc.OptimizeResources(opt);

            // Linearize the document for faster web viewing (first page loads quickly)
            doc.Optimize();

            // Save the optimized PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Optimized PDF saved to '{outputPath}'.");
    }
}