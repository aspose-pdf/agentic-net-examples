using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "compressed_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create and configure optimization options
            OptimizationOptions opt = new OptimizationOptions();

            // Enable image compression
            opt.ImageCompressionOptions.CompressImages = true;

            // Set compression quality (0‑100). 75 gives a good balance.
            opt.ImageCompressionOptions.ImageQuality = 75;

            // Resize images that exceed the specified resolution
            opt.ImageCompressionOptions.ResizeImages   = true;
            opt.ImageCompressionOptions.MaxResolution = 150; // DPI

            // Use the fast compression algorithm (optional)
            opt.ImageCompressionOptions.Version = ImageCompressionVersion.Fast; // enum value instead of raw int

            // Apply the optimization strategy to the document
            doc.OptimizeResources(opt);

            // Save the optimized PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Optimized PDF saved to '{outputPath}'.");
    }
}
