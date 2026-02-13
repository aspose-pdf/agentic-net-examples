using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization;

class PdfOptimizer
{
    static void Main(string[] args)
    {
        // Input and output file paths (generic names)
        string inputPath = "input.pdf";
        string outputPath = "output_optimized.pdf";

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
                CompressObjects = true,
                RemoveUnusedObjects = true,
                RemoveUnusedStreams = true,
                AllowReusePageContent = true
            };

            // Image compression settings
            opt.ImageCompressionOptions.CompressImages = true;
            // The ImageEncoding property is not available in older Aspose.Pdf versions;
            // JPEG is used by default, so we only set the quality.
            opt.ImageCompressionOptions.ImageQuality = 75; // 0‑100 (lower = smaller size)

            // Apply the optimization to the document
            pdfDocument.OptimizeResources(opt);

            // Save the optimized PDF
            pdfDocument.Save(outputPath);

            Console.WriteLine($"Optimization complete. Saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during optimization: {ex.Message}");
        }
    }
}
