using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "compressed.pdf";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle rule: use using for deterministic disposal)
            using (Document pdf = new Document(inputPath))
            {
                // Enable merging of identical resource streams (reduces size when adding pages)
                pdf.OptimizeSize = true;

                // Create an optimization strategy with all safe options activated
                OptimizationOptions opt = OptimizationOptions.All();

                // Fine‑tune the options: compress object streams, subset fonts, remove unused data, etc.
                opt.CompressObjects = true;          // pack objects into compressed streams
                opt.SubsetFonts = true;              // embed only used glyphs
                opt.RemoveUnusedObjects = true;      // delete objects not referenced by any page
                opt.RemoveUnusedStreams = true;      // delete unused resource streams
                opt.LinkDuplicateStreams = true;     // reuse identical streams

                // Apply the optimization to the document
                pdf.OptimizeResources(opt);

                // Linearize the PDF for faster web access (optional but helpful)
                pdf.Optimize();

                // Save the compressed PDF (lifecycle rule: use Document.Save)
                pdf.Save(outputPath);
            }

            Console.WriteLine($"Compressed PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}