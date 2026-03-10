using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        // Input PDF file paths
        const string firstPdfPath = "first.pdf";
        const string secondPdfPath = "second.pdf";

        // Output PDF that will contain the visual comparison result
        const string resultPdfPath = "comparison_result.pdf";

        // Verify that input files exist
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

        // Load the two documents inside using blocks for deterministic disposal
        using (Document doc1 = new Document(firstPdfPath))
        using (Document doc2 = new Document(secondPdfPath))
        {
            // Create the comparer instance
            GraphicalPdfComparer comparer = new GraphicalPdfComparer();

            // Perform the graphical comparison and save the result as a PDF
            comparer.CompareDocumentsToPdf(doc1, doc2, resultPdfPath);
        }

        Console.WriteLine($"Comparison PDF saved to '{resultPdfPath}'.");
    }
}