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

        // Open the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Keep existing metadata – do NOT call doc.RemoveMetadata().

            // Remove PDF/A and PDF/UA compliance flags (they are considered hidden content).
            doc.RemovePdfaCompliance();
            doc.RemovePdfUaCompliance();

            // Ensure signature fields are sanitized (default is true, set explicitly for clarity).
            doc.EnableSignatureSanitization = true;

            // Create an optimization strategy that removes all hidden data except metadata.
            OptimizationOptions opt = OptimizationOptions.All();
            opt.RemovePrivateInfo      = true;   // Remove private page information.
            opt.RemoveUnusedObjects    = true;   // Remove objects that are not referenced.
            opt.RemoveUnusedStreams    = true;   // Remove unused resource streams.
            opt.RemoveUnusedObjects    = true;   // Redundant but emphasizes intent.
            // The All() method already enables many safe options; we keep the above explicit settings.

            // Apply the optimization to the document.
            doc.OptimizeResources(opt);

            // Save the sanitized PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Sanitized PDF saved to '{outputPath}'.");
    }
}