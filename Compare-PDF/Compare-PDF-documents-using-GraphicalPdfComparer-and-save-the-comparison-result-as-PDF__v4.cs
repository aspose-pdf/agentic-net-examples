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
            // Instantiate the graphical comparer
            GraphicalPdfComparer comparer = new GraphicalPdfComparer();

            // Optional: customize comparison appearance
            // comparer.Color = Aspose.Pdf.Color.Red;      // change flag color
            // comparer.Resolution = 150;                  // DPI for internal images
            // comparer.Threshold = 0;                    // ignore small changes

            // Perform the comparison and write the result directly to a PDF file
            comparer.CompareDocumentsToPdf(doc1, doc2, resultPdfPath);
        }

        Console.WriteLine($"Graphical comparison saved to '{resultPdfPath}'.");
    }
}