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

        // Load the PDF inside a using block to ensure deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Configure optimization options (image compression, resolution, cleanup)
            OptimizationOptions opt = OptimizationOptions.All();

            // Image compression settings – use ImageCompressionOptions (new API)
            opt.ImageCompressionOptions.CompressImages = true;          // enable image compression
            opt.ImageCompressionOptions.ImageQuality = 75;               // quality 0‑100 (higher = better quality)
            opt.ImageCompressionOptions.ResizeImages = true;            // allow down‑sampling
            opt.ImageCompressionOptions.MaxResolution = 150;            // DPI limit for down‑sampling

            // Other cleanup options
            opt.RemoveUnusedObjects = true;      // strip unused PDF objects
            opt.RemoveUnusedStreams = true;      // strip unused streams

            // Apply the optimization to the document
            doc.OptimizeResources(opt);

            // Save the compressed PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Compressed PDF saved to '{outputPath}'.");
    }
}
