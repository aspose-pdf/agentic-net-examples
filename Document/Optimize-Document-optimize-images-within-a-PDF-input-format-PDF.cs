using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization;

class OptimizePdfImages
{
    static void Main(string[] args)
    {
        // Input and output PDF paths (adjust as needed)
        const string inputPath = "input.pdf";
        const string outputPath = "output_optimized.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPath);

            // Configure optimization options
            OptimizationOptions opt = new OptimizationOptions
            {
                RemoveUnusedObjects = true,
                RemoveUnusedStreams = true,
                CompressObjects = true,
                AllowReusePageContent = true,
                RemovePrivateInfo = true,
                SubsetFonts = true
            };

            // Image compression settings (ImageCompressionOptions is a nested object)
            opt.ImageCompressionOptions.CompressImages = true;
            opt.ImageCompressionOptions.ImageQuality = 75; // 0‑100
            opt.ImageCompressionOptions.Encoding = ImageEncoding.Jpeg;
            opt.ImageCompressionOptions.ResizeImages = true;
            opt.ImageCompressionOptions.MaxResolution = 150; // DPI

            // Apply the optimization to the document
            pdfDocument.OptimizeResources(opt);

            // Save the optimized PDF
            pdfDocument.Save(outputPath);

            Console.WriteLine($"Optimization completed. Output saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during optimization: {ex.Message}");
        }
    }
}
