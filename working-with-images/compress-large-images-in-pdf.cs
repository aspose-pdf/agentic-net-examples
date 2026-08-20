using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_compressed.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create an optimization strategy with all options enabled
            OptimizationOptions opt = OptimizationOptions.All();

            // Configure image compression: enable compression and set quality (e.g., 75%)
            opt.ImageCompressionOptions.CompressImages = true;
            opt.ImageCompressionOptions.ImageQuality = 75; // 0‑100, higher = better quality

            // Optionally limit the maximum resolution to avoid oversized images
            // opt.ImageCompressionOptions.MaxResolution = 1500; // uncomment if needed

            // Apply the optimization to the document
            doc.OptimizeResources(opt);

            // Save the optimized PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Optimized PDF saved to '{outputPath}'.");
    }
}