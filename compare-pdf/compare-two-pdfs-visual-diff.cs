using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        // Paths to the PDFs to be compared and the output file
        const string pdfPath1 = "original.pdf";
        const string pdfPath2 = "modified.pdf";
        const string outputPath = "comparison_result.pdf";

        // Verify that both input files exist
        if (!File.Exists(pdfPath1) || !File.Exists(pdfPath2))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load the two documents inside using blocks for deterministic disposal
        using (Document doc1 = new Document(pdfPath1))
        using (Document doc2 = new Document(pdfPath2))
        {
            // Configure comparison options – adjust as needed for multimedia differences
            var options = new SideBySideComparisonOptions
            {
                // The current Aspose.PDF version does not expose a HighlightColor property.
                // Default visual settings will be used. Add any supported options here.
            };

            // Perform side‑by‑side comparison and generate a PDF showing the differences.
            // SideBySidePdfComparer is a static class; its Compare method is also static.
            SideBySidePdfComparer.Compare(doc1, doc2, outputPath, options);
        }

        Console.WriteLine($"Comparison completed. Result saved to '{outputPath}'.");
    }
}
