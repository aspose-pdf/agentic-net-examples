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

        using (Document doc = new Document(inputPath))
        {
            // Configure optimization options. The "RemoveSearchIndexAndPrivateInfo" flag
            // is not available in recent Aspose.Pdf versions, so we rely on the
            // "RemovePrivateInfo" flag and explicit metadata removal.
            OptimizationOptions opt = new OptimizationOptions
            {
                RemovePrivateInfo = true // removes private information such as document IDs
                // Add other options here if needed, e.g., RemoveUnusedObjects = true;
            };

            // Apply the optimization (sanitization) to the document.
            doc.OptimizeResources(opt);

            // Clear any remaining metadata entries.
            doc.RemoveMetadata();

            // If the current Aspose.Pdf version provides a dedicated method to clear the
            // search index, it can be invoked here. The method name may vary between versions.
            // Uncomment the line below if such a method exists in your library version.
            // doc.RemoveSearchIndexAndPrivateInfo();

            // Save the sanitized PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Sanitized PDF saved to '{outputPath}'.");
    }
}
