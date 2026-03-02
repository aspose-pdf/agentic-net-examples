using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string firstPdfPath  = "document1.pdf";
        const string secondPdfPath = "document2.pdf";
        const string resultPdfPath = "comparison_result.pdf";

        // Verify that both source files exist before proceeding
        if (!File.Exists(firstPdfPath) || !File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load the two PDFs inside using blocks to ensure proper disposal
        using (Document doc1 = new Document(firstPdfPath))
        using (Document doc2 = new Document(secondPdfPath))
        {
            // Create comparison options (default settings can be adjusted if needed)
            SideBySideComparisonOptions options = new SideBySideComparisonOptions();

            // Perform side‑by‑side comparison; the result is written directly to resultPdfPath
            SideBySidePdfComparer.Compare(doc1, doc2, resultPdfPath, options);
        }

        Console.WriteLine($"Side‑by‑side comparison PDF saved to '{resultPdfPath}'.");
    }
}