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
        using (Document pdfDoc = new Document(inputPath))
        {
            // Create and configure optimization options
            OptimizationOptions opt = new OptimizationOptions();

            // General PDF object compression
            opt.CompressObjects = true;
            opt.RemoveUnusedObjects = true;
            opt.RemoveUnusedStreams = true;
            opt.LinkDuplicateStreams = true;

            // Image compression settings
            opt.ImageCompressionOptions.CompressImages = true;
            opt.ImageCompressionOptions.ImageQuality = 75;          // 0‑100, higher = better quality
            opt.ImageCompressionOptions.MaxResolution = 150;       // DPI, images above this are downscaled
            opt.ImageCompressionOptions.ResizeImages = true;
            // The ImageCompressionVersion enum does not expose a "Version1" member in recent SDK versions.
            // The default version is sufficient for most scenarios, so we omit setting it explicitly.

            // Apply the optimization strategy to the document
            pdfDoc.OptimizeResources(opt);

            // Save the optimized PDF
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"Optimized PDF saved to '{outputPath}'.");
    }
}
