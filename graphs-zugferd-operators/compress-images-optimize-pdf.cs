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
            // Set image quality (0‑100, higher = better quality)
            opt.ImageCompressionOptions.ImageQuality = 75;

            // Optional: limit maximum image resolution (DPI)
            opt.MaxResoultion = 150;

            // Additional size‑reduction settings
            opt.RemoveUnusedObjects   = true;
            opt.RemoveUnusedStreams   = true;
            opt.LinkDuplicateStreams  = true;

            // Apply the optimization strategy to the document
            doc.OptimizeResources(opt);

            // Save the optimized PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Optimized PDF saved to '{outputPath}'.");
    }
}