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
            // Create optimization options
            OptimizationOptions opt = new OptimizationOptions();

            // Enable image compression
            opt.ImageCompressionOptions.CompressImages = true;
            // Set JPEG quality (0‑100). Lower value = higher compression.
            opt.ImageCompressionOptions.ImageQuality = 75;
            // Optionally limit the maximum resolution of images (dpi)
            opt.ImageCompressionOptions.MaxResolution = 150;

            // Apply the optimization to the document
            doc.OptimizeResources(opt);

            // Optional: merge identical resource streams to further reduce size
            doc.OptimizeSize = true;

            // Save the optimized PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Compressed PDF saved to '{outputPath}'.");
    }
}