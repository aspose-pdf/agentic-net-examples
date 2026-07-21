using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "sanitized.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // -----------------------------------------------------------------
            // 1. Remove hidden content while preserving metadata.
            //    - Use OptimizationOptions to strip private info, unused objects
            //      and unused streams.
            //    - Remove PDF/A and PDF/UA compliance flags (they are hidden
            //      compliance data).
            //    - Do NOT call RemoveMetadata() because we want to keep the
            //      document metadata.
            // -----------------------------------------------------------------

            // Create an optimization strategy with all non‑functional options enabled
            OptimizationOptions opt = OptimizationOptions.All();

            // Activate removal of hidden data (except metadata)
            opt.RemovePrivateInfo   = true;   // removes page piece info
            opt.RemoveUnusedObjects = true;   // removes objects with no references
            opt.RemoveUnusedStreams = true;   // removes resources never used

            // Apply the optimization to the document
            doc.OptimizeResources(opt);

            // Remove PDF/A and PDF/UA compliance information (these are hidden
            // compliance markers that we want to discard)
            doc.RemovePdfaCompliance();
            doc.RemovePdfUaCompliance();

            // Ensure signature sanitization is enabled (default is true)
            doc.EnableSignatureSanitization = true;

            // -----------------------------------------------------------------
            // 2. Save the sanitized PDF.
            // -----------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"Sanitized PDF saved to '{outputPath}'.");
    }
}