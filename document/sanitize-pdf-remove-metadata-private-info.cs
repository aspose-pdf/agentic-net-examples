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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Remove all standard metadata entries
            doc.RemoveMetadata();

            // Remove any PDF/A or PDF/UA compliance flags that may retain extra information
            doc.RemovePdfaCompliance();
            doc.RemovePdfUaCompliance();

            // Configure optimization to strip private information and unused resources
            OptimizationOptions opt = new OptimizationOptions
            {
                RemovePrivateInfo   = true,   // clears private page piece info
                RemoveUnusedObjects = true,   // deletes objects with no references
                RemoveUnusedStreams = true    // removes unused resource streams
            };

            // Apply the optimization strategy
            doc.OptimizeResources(opt);

            // Save the sanitized PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Sanitized PDF saved to '{outputPath}'.");
    }
}