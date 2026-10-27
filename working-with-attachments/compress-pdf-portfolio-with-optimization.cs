using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization;

class Program
{
    static void Main()
    {
        // Paths for the source portfolio PDF and the compressed output PDF.
        const string inputPath  = "portfolio_input.pdf";
        const string outputPath = "portfolio_compressed.pdf";

        // Verify that the source file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF Portfolio.
        Document portfolio = new Document(inputPath);

        // Configure optimization options to achieve compression.
        OptimizationOptions opt = OptimizationOptions.All();
        opt.CompressImages = true;          // compress embedded images
        opt.ImageQuality = 50;              // optional: set image quality (0‑100)
        opt.RemoveUnusedObjects = true;    // drop objects that are not referenced
        opt.RemoveUnusedStreams = true;    // drop unused streams

        // Apply the optimization to the document.
        portfolio.OptimizeResources(opt);

        // Save the optimized (compressed) PDF Portfolio.
        portfolio.Save(outputPath);

        // Clean up.
        portfolio.Dispose();

        Console.WriteLine($"Compressed PDF Portfolio saved to '{outputPath}'.");
    }
}
