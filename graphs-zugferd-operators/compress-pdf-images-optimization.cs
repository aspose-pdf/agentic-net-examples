using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "compressed_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create optimization options
            OptimizationOptions opt = new OptimizationOptions();

            // Configure image compression options
            ImageCompressionOptions imgOpts = opt.ImageCompressionOptions;
            imgOpts.CompressImages = true;      // Enable image compression
            imgOpts.ImageQuality = 75;          // Quality level (0-100)
            imgOpts.ResizeImages = true;       // Allow resizing of high‑resolution images
            imgOpts.MaxResolution = 150;       // Maximum DPI for images

            // Additional optimizations (optional)
            opt.CompressObjects = true;        // Compress PDF objects into streams
            opt.RemoveUnusedObjects = true;    // Remove objects not referenced by any page
            opt.SubsetFonts = true;            // Embed only used glyphs

            // Apply the optimization strategy
            doc.OptimizeResources(opt);

            // Save the optimized PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Optimized PDF saved to '{outputPath}'.");
    }
}