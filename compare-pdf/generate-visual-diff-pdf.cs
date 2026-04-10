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
        const string resultPdfPath = "visual_diff.pdf";

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
            // Create the comparer instance
            GraphicalPdfComparer comparer = new GraphicalPdfComparer();

            // Optional: customize comparison appearance
            // comparer.Color = Aspose.Pdf.Color.Red;      // change flag color
            // comparer.Threshold = 5;                     // ignore changes <5%

            // Perform the visual comparison and generate the result PDF
            comparer.CompareDocumentsToPdf(doc1, doc2, resultPdfPath);
        }

        Console.WriteLine($"Visual diff PDF created at '{resultPdfPath}'.");
    }
}