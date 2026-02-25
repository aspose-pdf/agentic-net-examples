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
        const string outputPdfPath = "comparison_result.pdf";

        // Verify that both source files exist
        if (!File.Exists(firstPdfPath) || !File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load the two PDFs inside using blocks to ensure deterministic disposal
        using (Document doc1 = new Document(firstPdfPath))
        using (Document doc2 = new Document(secondPdfPath))
        {
            // Create default comparison options (customize if needed)
            ComparisonOptions options = new ComparisonOptions();

            // Perform a page‑by‑page text comparison.
            // The overload with a string argument saves the visual diff directly to a PDF file.
            TextPdfComparer.CompareDocumentsPageByPage(doc1, doc2, options, outputPdfPath);
        }

        Console.WriteLine($"Comparison PDF saved to '{outputPdfPath}'.");
    }
}