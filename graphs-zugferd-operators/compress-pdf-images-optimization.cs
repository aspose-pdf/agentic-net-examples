using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_compressed.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Create optimization options
            OptimizationOptions opt = new OptimizationOptions();

            // Enable image compression and set quality
            opt.ImageCompressionOptions.CompressImages = true;
            opt.ImageCompressionOptions.ImageQuality = 75; // 0-100, higher = better quality
            opt.ImageCompressionOptions.ResizeImages = true;
            opt.ImageCompressionOptions.MaxResolution = 150; // DPI, images above this will be downscaled

            // Additional optimizations (optional but helpful)
            opt.CompressObjects = true;          // Compress PDF object streams
            opt.RemoveUnusedObjects = true;      // Remove objects not referenced by any page
            opt.SubsetFonts = true;              // Embed only used glyphs

            // Apply the optimization strategy to the document
            pdfDoc.OptimizeResources(opt);

            // Save the optimized PDF
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"Optimized PDF saved to '{outputPath}'.");
    }
}