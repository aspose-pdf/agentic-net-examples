using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string firstPdfPath  = "first.pdf";
        const string secondPdfPath = "second.pdf";
        const string resultPdfPath = "comparison_result.pdf";

        // Verify that both source files exist before proceeding.
        if (!File.Exists(firstPdfPath) || !File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load the two documents inside using blocks to ensure deterministic disposal.
        using (Document doc1 = new Document(firstPdfPath))
        using (Document doc2 = new Document(secondPdfPath))
        {
            // Create comparison options. Default settings are sufficient for a basic side‑by‑side compare.
            SideBySideComparisonOptions options = new SideBySideComparisonOptions();

            // Perform the side‑by‑side comparison. The method writes the result directly to the target file.
            SideBySidePdfComparer.Compare(doc1, doc2, resultPdfPath, options);
        }

        Console.WriteLine($"Side‑by‑side comparison PDF saved to '{resultPdfPath}'.");
    }
}