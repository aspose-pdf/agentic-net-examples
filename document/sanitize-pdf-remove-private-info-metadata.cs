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

        // Load the PDF, apply sanitization options, and save.
        using (Document doc = new Document(inputPath))
        {
            // Remove private information (page piece info) and clear metadata.
            OptimizationOptions opt = new OptimizationOptions
            {
                RemovePrivateInfo = true
            };
            doc.OptimizeResources(opt);
            doc.RemoveMetadata();

            // Save the sanitized PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Sanitized PDF saved to '{outputPath}'.");
    }
}