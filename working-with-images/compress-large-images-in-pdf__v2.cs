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

        // Load the PDF document (lifecycle rule: load)
        using (Document doc = new Document(inputPath))
        {
            // Create optimization options. Some options are set via object initializer,
            // but ImageCompressionOptions is read‑only, so we modify its instance later.
            OptimizationOptions opt = new OptimizationOptions
            {
                RemoveUnusedObjects = true,
                CompressObjects = true
                // other options can be added here if needed
            };

            // Configure image compression on the existing ImageCompressionOptions instance.
            opt.ImageCompressionOptions.CompressImages = true;
            opt.ImageCompressionOptions.ImageQuality = 75; // 0‑100, lower = smaller size
            // Optional: set the encoding (e.g., JPEG) if you want explicit control.
            // opt.ImageCompressionOptions.Encoding = ImageEncoding.Jpeg;

            // Apply the optimization to the document.
            doc.OptimizeResources(opt);

            // Save the optimized PDF (lifecycle rule: save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Optimized PDF saved to '{outputPath}'.");
    }
}