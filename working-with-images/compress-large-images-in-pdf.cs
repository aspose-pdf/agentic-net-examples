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

        // Load the PDF document (using the lifecycle rule)
        using (Document doc = new Document(inputPath))
        {
            // Configure optimization options to compress images.
            // This will replace large images with compressed JPEG versions.
            OptimizationOptions opt = new OptimizationOptions
            {
                // Merge identical objects to reduce size.
                CompressObjects = true,

                // Configure image compression.
                ImageCompressionOptions =
                {
                    CompressImages = true,   // Enable image compression.
                    ImageQuality   = 75,     // JPEG quality (0‑100). Adjust as needed.
                    // MaxResolution = 150; // Optional: limit image resolution (DPI).
                }
            };

            // Apply the optimization to the document.
            doc.OptimizeResources(opt);

            // Optional: enable additional stream merging.
            doc.OptimizeSize = true;

            // Save the optimized PDF (using the lifecycle rule).
            doc.Save(outputPath);
        }

        Console.WriteLine($"Compressed PDF saved to '{outputPath}'.");
    }
}