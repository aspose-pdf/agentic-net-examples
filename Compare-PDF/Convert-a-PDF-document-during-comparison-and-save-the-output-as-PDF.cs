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

        // Verify that both source files exist
        if (!File.Exists(firstPdfPath) || !File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load both PDFs inside using blocks for deterministic disposal
        using (Document doc1 = new Document(firstPdfPath))
        using (Document doc2 = new Document(secondPdfPath))
        {
            // Create the comparer instance
            GraphicalPdfComparer comparer = new GraphicalPdfComparer();

            // Optional: configure comparison appearance
            // comparer.Color = Aspose.Pdf.Color.Red;
            // comparer.Resolution = 150;
            // comparer.Threshold = 0; // percentage

            // Perform the graphical comparison and save the result as a PDF
            comparer.CompareDocumentsToPdf(doc1, doc2, resultPdfPath);
        }

        Console.WriteLine($"Comparison PDF saved to '{resultPdfPath}'.");
    }
}