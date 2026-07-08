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

        // Load the PDF using the core Document API.
        using (Document doc = new Document(inputPath))
        {
            // Create an optimization strategy that activates all non‑functional options.
            // Then enable the specific options that remove hidden data but keep metadata.
            OptimizationOptions opts = OptimizationOptions.All();

            // Remove private information (page piece info) and any unused objects/streams.
            // This eliminates most hidden content while preserving document metadata.
            opts.RemovePrivateInfo   = true;
            opts.RemoveUnusedObjects = true;
            opts.RemoveUnusedStreams = true;

            // Apply the optimization to the document.
            doc.OptimizeResources(opts);

            // Optionally, disable signature sanitization if signatures should be preserved.
            // By default it is enabled; setting to false keeps signature fields unchanged.
            doc.EnableSignatureSanitization = false;

            // Save the sanitized PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Sanitized PDF saved to '{outputPath}'.");
    }
}