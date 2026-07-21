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

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Create optimization options
            OptimizationOptions opt = new OptimizationOptions();

            // Enable image compression and set JPEG quality (e.g., 75%)
            opt.ImageCompressionOptions.CompressImages = true;
            opt.ImageCompressionOptions.ImageQuality   = 75; // 0‑100, higher = better quality

            // Optionally limit the maximum resolution to avoid up‑scaling large images
            opt.ImageCompressionOptions.MaxResolution = 1500; // DPI (adjust as needed)

            // Apply the optimization to the document.
            // This will replace images (including those >2 MB) with compressed JPEG versions.
            doc.OptimizeResources(opt);

            // Save the optimized PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Optimized PDF saved to '{outputPath}'.");
    }
}