using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison; // Comparison classes are in this namespace

class Program
{
    static void Main()
    {
        const string firstPdfPath  = "first.pdf";
        const string secondPdfPath = "second.pdf";
        const string resultPdfPath = "comparison_result.pdf";

        // Verify that both source files exist
        if (!File.Exists(firstPdfPath) || !File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load the two documents inside using blocks for deterministic disposal
        using (Document doc1 = new Document(firstPdfPath))
        using (Document doc2 = new Document(secondPdfPath))
        {
            // GraphicalPdfComparer provides a visual (pixel‑level) comparison.
            // The result is written directly to a PDF file.
            GraphicalPdfComparer comparer = new GraphicalPdfComparer();
            comparer.CompareDocumentsToPdf(doc1, doc2, resultPdfPath);
        }

        Console.WriteLine($"Comparison PDF saved to '{resultPdfPath}'.");
    }
}