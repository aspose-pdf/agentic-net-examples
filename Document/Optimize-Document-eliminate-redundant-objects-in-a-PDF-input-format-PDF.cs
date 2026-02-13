using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization;

class Program
{
    static void Main(string[] args)
    {
        string inputPath = "input.pdf";
        string outputPath = "output_optimized.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPath);

            // Set up optimization options to remove redundant objects
            OptimizationOptions opt = new OptimizationOptions
            {
                RemoveUnusedObjects = true,      // delete objects with no references
                RemoveUnusedStreams = true,      // delete unused resource streams
                AllowReusePageContent = true,    // reuse identical page content
                CompressObjects = true,          // pack objects into compressed streams
                LinkDuplicateStreams = true,     // merge duplicate streams
                SubsetFonts = true,              // keep only used glyphs in embedded fonts
                UnembedFonts = false             // keep required fonts embedded
            };

            // Optimize the document
            pdfDocument.OptimizeResources(opt);

            // Save the optimized PDF
            pdfDocument.Save(outputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Optimization failed: {ex.Message}");
        }
    }
}