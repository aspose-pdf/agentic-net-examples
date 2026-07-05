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

        // Load the PDF document inside a using block (ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Configure optimization options.
            // - CompressObjects packs PDF objects into streams.
            // - ImageCompressionOptions enables image compression and sets a quality level.
            //   Images larger than the default threshold will be recompressed as JPEG.
            OptimizationOptions opt = new OptimizationOptions
            {
                CompressObjects = true,
                RemoveUnusedObjects = true // optional, helps reduce size further
            };

            // The ImageCompressionOptions property is read‑only; retrieve the existing instance
            // and set its individual members.
            ImageCompressionOptions imgOpt = opt.ImageCompressionOptions;
            imgOpt.CompressImages = true;      // enable image compression
            imgOpt.ImageQuality   = 50;        // JPEG quality (0‑100)
            imgOpt.MaxResolution  = 150;       // downscale images above this DPI
            // Optional: force JPEG encoding for better compression
            imgOpt.Encoding = ImageEncoding.Jpeg;

            // Apply the optimization to the document.
            doc.OptimizeResources(opt);

            // Save the optimized PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Compressed PDF saved to '{outputPath}'.");
    }
}
