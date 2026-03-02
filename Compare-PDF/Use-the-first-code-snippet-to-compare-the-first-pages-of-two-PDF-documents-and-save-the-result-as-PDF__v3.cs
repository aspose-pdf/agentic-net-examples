using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string firstPdfPath = "first.pdf";
        const string secondPdfPath = "second.pdf";
        const string resultPdfPath = "comparison_result.pdf";

        if (!File.Exists(firstPdfPath) || !File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load both PDFs with deterministic disposal
        using (Document doc1 = new Document(firstPdfPath))
        using (Document doc2 = new Document(secondPdfPath))
        {
            // Pages are 1‑based indexed
            Page page1 = doc1.Pages[1];
            Page page2 = doc2.Pages[1];

            // Compare the first pages and write the visual diff to a new PDF
            GraphicalPdfComparer comparer = new GraphicalPdfComparer();
            comparer.ComparePagesToPdf(page1, page2, resultPdfPath);
        }

        Console.WriteLine($"Comparison PDF saved to '{resultPdfPath}'.");
    }
}