using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "sanitized_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Remove PDF/A and PDF/UA compliance flags if present
            doc.RemovePdfaCompliance();
            doc.RemovePdfUaCompliance();

            // Remove all standard metadata (Info dictionary)
            doc.RemoveMetadata();

            // Create optimization options to strip private information and unused resources
            OptimizationOptions opt = new OptimizationOptions
            {
                RemovePrivateInfo   = true,   // clears private info such as search index data
                RemoveUnusedObjects = true,   // removes objects that are not referenced
                RemoveUnusedStreams = true    // removes unused resource streams
            };

            // Apply the optimization to the document
            doc.OptimizeResources(opt);

            // Save the sanitized PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Sanitized PDF saved to '{outputPath}'.");
    }
}