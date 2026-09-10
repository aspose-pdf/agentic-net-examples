using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "cleaned.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF, process, and save – all within a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // 1. Remove standard metadata (author, title, etc.) and hidden metadata entries
            doc.RemoveMetadata();

            // 2. Remove all embedded files (attachments) from the document, if any
            //    The EmbeddedFiles collection provides a Delete() method that clears them.
            doc.EmbeddedFiles?.Delete();

            // 3. (Best‑effort) Remove JavaScript actions.
            //    Aspose.Pdf does not expose a dedicated API for stripping JavaScript,
            //    but calling OptimizeResources with default options removes unused objects,
            //    which includes most script objects that are not referenced.
            OptimizationOptions opt = OptimizationOptions.All();
            doc.OptimizeResources(opt);

            // 4. Optional: remove PDF/A and PDF/UA compliance flags that may retain extra data
            doc.RemovePdfaCompliance();
            doc.RemovePdfUaCompliance();

            // Save the cleaned PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Cleaned PDF saved to '{outputPath}'.");
    }
}