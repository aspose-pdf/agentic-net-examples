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

        // Load the PDF document.
        using (Document doc = new Document(inputPath))
        {
            // Configure optimization to compress images.
            OptimizationOptions opt = new OptimizationOptions();

            // Enable image compression.
            opt.ImageCompressionOptions.CompressImages = true;

            // Set JPEG quality (0‑100). Lower value yields higher compression.
            opt.ImageCompressionOptions.ImageQuality = 70;

            // Optional: limit maximum image resolution (DPI) to avoid up‑scaling.
            opt.ImageCompressionOptions.MaxResolution = 1500;

            // Apply the optimization to the whole document.
            doc.OptimizeResources(opt);

            // Save the optimized PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Compressed PDF saved to '{outputPath}'.");
    }
}