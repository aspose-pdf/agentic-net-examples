using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "sanitized.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Remove all metadata entries
            doc.RemoveMetadata();

            // Configure optimization to clear private information (search index, etc.)
            OptimizationOptions opt = new OptimizationOptions
            {
                RemovePrivateInfo = true,
                RemoveUnusedObjects = true,
                RemoveUnusedStreams = true
            };

            // Apply the optimization settings
            doc.OptimizeResources(opt);

            // Save the sanitized PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Sanitized PDF saved to '{outputPath}'.");
    }
}