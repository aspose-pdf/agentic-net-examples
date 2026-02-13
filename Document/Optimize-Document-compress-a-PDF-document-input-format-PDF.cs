using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths
        const string inputPath = "input.pdf";
        const string outputPath = "output_optimized.pdf";

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
            // Reuse identical page content when possible
            AllowReusePageContent = true,

            // Compress PDF objects into streams
            CompressObjects = true,

            // Remove objects that are not referenced
            RemoveUnusedObjects = true,

            // Remove resource streams that are never used
            RemoveUnusedStreams = true,

            // Subset embedded fonts to keep only used glyphs
            SubsetFonts = true,

            // Keep fonts embedded (set to false to unembed)
            UnembedFonts = false,

            // Remove private information (e.g., page piece info)
            RemovePrivateInfo = true,

            // Detect and merge duplicate resource streams
            LinkDuplicateStreams = true,

            // Limit image resolution (DPI) – higher images will be down‑scaled
            MaxResoultion = 150
        };

        // Image‑specific compression settings (ImageCompressionOptions is read‑only,
        // so we modify its properties directly)
        opt.ImageCompressionOptions.CompressImages = true;
        opt.ImageCompressionOptions.ImageQuality = 75; // 0‑100, higher = better quality

        // Apply the optimization to the document
        pdfDocument.OptimizeResources(opt);

        // Save the optimized PDF
        pdfDocument.Save(outputPath);
    }
}