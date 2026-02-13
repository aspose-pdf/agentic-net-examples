using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPath = "input.pdf";
        // Output optimized PDF file path
        const string outputPath = "optimized.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPath);

            // Configure optimization options
            OptimizationOptions opt = new OptimizationOptions
            {
                // Reuse identical page content when possible
                AllowReusePageContent = true,
                // Compress PDF objects into streams
                CompressObjects = true,
                // Remove objects that are not referenced
                RemoveUnusedObjects = true,
                // Remove streams that are not used
                RemoveUnusedStreams = true,
                // Remove private information (e.g., page piece info)
                RemovePrivateInfo = true,
                // Subset fonts to keep only used glyphs
                SubsetFonts = true,
                // Unembed fonts if you prefer them not to be embedded
                UnembedFonts = false,
                // Enable detection and removal of duplicate streams
                LinkDuplicateStreams = true,
                // Set a maximum resolution for images (optional)
                MaxResoultion = 150
            };

            // Optimize the PDF using the configured options
            pdfDocument.OptimizeResources(opt);

            // Save the optimized PDF
            pdfDocument.Save(outputPath);

            Console.WriteLine($"Optimization complete. Saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}