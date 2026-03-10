using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        // Paths to the PDFs to be compared and the output PDF
        const string pdfPath1 = "doc1.pdf";
        const string pdfPath2 = "doc2.pdf";
        const string resultPath = "comparison_result.pdf";

        // Verify that both input files exist
        if (!File.Exists(pdfPath1) || !File.Exists(pdfPath2))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load the two PDF documents inside using blocks for deterministic disposal
        using (Document doc1 = new Document(pdfPath1))
        using (Document doc2 = new Document(pdfPath2))
        {
            // Create default comparison options; customize if needed (e.g., colors, thresholds)
            SideBySideComparisonOptions options = new SideBySideComparisonOptions();

            // Perform side‑by‑side comparison and write the result directly to a PDF file
            SideBySidePdfComparer.Compare(doc1, doc2, resultPath, options);
        }

        Console.WriteLine($"Comparison PDF saved to '{resultPath}'.");
    }
}