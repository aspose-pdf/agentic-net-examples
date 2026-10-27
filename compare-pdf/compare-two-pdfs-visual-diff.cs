using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        // Paths to the PDFs to be compared and the output file
        const string firstPdfPath  = "original.pdf";
        const string secondPdfPath = "modified.pdf";
        const string outputPath    = "comparison_result.pdf";

        // Verify that both input files exist
        if (!File.Exists(firstPdfPath) || !File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load the two documents inside using blocks for deterministic disposal
        using (Document doc1 = new Document(firstPdfPath))
        using (Document doc2 = new Document(secondPdfPath))
        {
            // Configure side‑by‑side comparison options (default settings are sufficient for most cases)
            SideBySideComparisonOptions compareOptions = new SideBySideComparisonOptions
            {
                // Example: show differences with a red overlay
                // HighlightColor = Aspose.Pdf.Color.Red,
                // ShowDifferences = true
            };

            // Perform the visual side‑by‑side comparison.
            // The Compare method is static, returns void and requires the output file path as the third argument.
            SideBySidePdfComparer.Compare(doc1, doc2, outputPath, compareOptions);
        }

        Console.WriteLine($"Comparison completed. Result saved to '{outputPath}'.");
    }
}
