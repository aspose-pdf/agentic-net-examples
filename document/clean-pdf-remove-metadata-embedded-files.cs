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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // 1. Remove all document metadata (including hidden metadata)
            doc.RemoveMetadata();

            // 2. Remove PDF/A and PDF/UA compliance (if present) to allow further optimizations
            doc.RemovePdfaCompliance();
            doc.RemovePdfUaCompliance();

            // 3. Delete all embedded files (attachments) from the document
            //    The EmbeddedFiles collection provides Delete() methods.
            //    Delete() without parameters removes all embedded files.
            doc.EmbeddedFiles?.Delete();

            // 4. Optimize resources: remove unused objects, compress streams, etc.
            //    Use the default optimization options (all safe options enabled).
            OptimizationOptions opt = OptimizationOptions.All();
            doc.OptimizeResources(opt);

            // 5. Linearize the document for faster web access (optional but reduces size)
            doc.Optimize();

            // Save the cleaned PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Cleaned PDF saved to '{outputPath}'.");
    }
}