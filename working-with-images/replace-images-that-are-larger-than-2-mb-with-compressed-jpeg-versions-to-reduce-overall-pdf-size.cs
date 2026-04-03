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
        using (Document pdfDoc = new Document(inputPath))
        {
            // Configure optimization options
            OptimizationOptions optOptions = new OptimizationOptions();

            // Enable image compression
            optOptions.ImageCompressionOptions.CompressImages = true;

            // Set desired JPEG quality (0‑100). Lower value = higher compression.
            optOptions.ImageCompressionOptions.ImageQuality = 75;

            // Optional: limit maximum resolution; images with higher DPI will be down‑scaled.
            // This helps to reduce size for very large images.
            optOptions.ImageCompressionOptions.MaxResolution = 300; // DPI

            // Apply the optimization to the document
            pdfDoc.OptimizeResources(optOptions);

            // Save the optimized PDF
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"Compressed PDF saved to '{outputPath}'.");
    }
}