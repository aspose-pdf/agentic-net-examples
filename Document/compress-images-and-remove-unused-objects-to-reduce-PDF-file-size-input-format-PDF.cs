using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_optimized.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Create and configure optimization options
                OptimizationOptions opt = new OptimizationOptions
                {
                    // General resource optimizations
                    CompressObjects = true,
                    RemoveUnusedObjects = true,
                    RemoveUnusedStreams = true,
                    LinkDuplicateStreams = true,
                    AllowReusePageContent = true,
                    SubsetFonts = true,
                    UnembedFonts = false
                };

                // Image-specific compression settings
                opt.ImageCompressionOptions.CompressImages = true;
                opt.ImageCompressionOptions.ImageQuality = 75;      // 0‑100
                opt.ImageCompressionOptions.ResizeImages = true;
                opt.ImageCompressionOptions.MaxResolution = 150;   // DPI

                // Apply the optimization strategy to the document
                doc.OptimizeResources(opt);

                // Save the optimized PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Optimized PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}