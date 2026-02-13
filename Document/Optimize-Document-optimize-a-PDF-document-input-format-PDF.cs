using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization;

class OptimizePdfExample
{
    static void Main(string[] args)
    {
        // Input and output PDF file paths
        string inputPath = "input.pdf";
        string outputPath = "output_optimized.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(inputPath);

        // Configure optimization options
        OptimizationOptions opt = new OptimizationOptions
        {
            // Reuse identical page contents to reduce size
            AllowReusePageContent = true,

            // Compress PDF objects into streams
            CompressObjects = true,

            // Remove objects that are not referenced anywhere
            RemoveUnusedObjects = true,

            // Remove resource streams that are never used
            RemoveUnusedStreams = true,

            // Remove private information (e.g., page piece info)
            RemovePrivateInfo = true,

            // Detect and merge duplicate resource streams
            LinkDuplicateStreams = true,

            // Subset embedded fonts to only used glyphs
            SubsetFonts = true,

            // Keep fonts embedded (set to false to unembed)
            UnembedFonts = false,

            // Limit maximum image resolution (higher values keep more detail)
            MaxResoultion = 150,

            // Choose image encoding for compressed images
            ImageEncoding = ImageEncoding.Jpeg
        };

        // Apply the optimization to the document
        pdfDocument.OptimizeResources(opt);

        // Save the optimized PDF (uses the provided document-save rule)
        pdfDocument.Save(outputPath);

        Console.WriteLine($"Optimization complete. Saved to: {outputPath}");
    }
}