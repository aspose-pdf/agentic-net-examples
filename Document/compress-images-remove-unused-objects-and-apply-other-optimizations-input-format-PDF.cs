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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Linearize the document for fast web viewing
            doc.Optimize();

            // Create an optimization strategy with all generic options enabled
            OptimizationOptions opt = OptimizationOptions.All();

            // Enable compression of object streams
            opt.CompressObjects = true;

            // Remove objects and streams that are not referenced anywhere
            opt.RemoveUnusedObjects = true;
            opt.RemoveUnusedStreams = true;

            // Merge duplicate resource streams to reduce size
            opt.LinkDuplicateStreams = true;

            // Subset fonts so only used glyphs are embedded
            opt.SubsetFonts = true;

            // Optionally downscale high‑resolution images (e.g., to 150 DPI)
            opt.MaxResoultion = 150;

            // Encode images as JPEG for additional compression
            opt.ImageEncoding = ImageEncoding.Jpeg;

            // Apply the resource optimization with the configured options
            doc.OptimizeResources(opt);

            // Save the optimized PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Optimized PDF saved to '{outputPath}'.");
    }
}