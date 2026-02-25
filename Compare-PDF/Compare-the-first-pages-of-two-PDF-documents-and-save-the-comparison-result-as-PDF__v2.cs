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
        const string outputPath    = "first_pages_comparison.pdf";

        // Verify input files exist
        if (!File.Exists(firstPdfPath))
        {
            Console.Error.WriteLine($"File not found: {firstPdfPath}");
            return;
        }
        if (!File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine($"File not found: {secondPdfPath}");
            return;
        }

        // Load both documents inside using blocks for deterministic disposal
        using (Document doc1 = new Document(firstPdfPath))
        using (Document doc2 = new Document(secondPdfPath))
        {
            // Aspose.Pdf uses 1‑based page indexing
            Page page1 = doc1.Pages[1];
            Page page2 = doc2.Pages[1];

            // Default side‑by‑side comparison options
            SideBySideComparisonOptions options = new SideBySideComparisonOptions();

            // Perform the comparison and save the result as a PDF
            SideBySidePdfComparer.Compare(page1, page2, outputPath, options);
        }

        Console.WriteLine($"Comparison saved to '{outputPath}'.");
    }
}