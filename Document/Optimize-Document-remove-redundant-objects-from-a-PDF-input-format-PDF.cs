using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization;

class Program
{
    static void Main(string[] args)
    {
        // Input and output PDF file paths (adjust as needed)
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
                // Reuse identical page content when possible
                AllowReusePageContent = true,

                // Compress PDF objects into streams
                CompressObjects = true,

                // Remove objects that are not referenced anywhere
                RemoveUnusedObjects = true,

                // Remove resource streams that are never used
                RemoveUnusedStreams = true,

                // Merge duplicate resource streams
                LinkDuplicateStreams = true,

                // Subset embedded fonts to keep only used glyphs
                SubsetFonts = true,

                // Optionally unembed fonts (set to true to make fonts non‑embedded)
                UnembedFonts = false,

                // Limit maximum image resolution (0 = no limit)
                MaxResoultion = 0
            };

            // Apply the optimization to the document
            pdfDocument.OptimizeResources(opt);

            // Save the optimized PDF (uses the provided document-save rule)
            pdfDocument.Save(outputPath);

            Console.WriteLine($"Optimization completed. Output saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during optimization: {ex.Message}");
        }
    }
}