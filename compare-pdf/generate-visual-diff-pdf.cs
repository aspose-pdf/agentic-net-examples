using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison; // GraphicalPdfComparer resides here

class Program
{
    static void Main()
    {
        const string firstPdfPath  = "first.pdf";
        const string secondPdfPath = "second.pdf";
        const string resultPdfPath = "visual_diff.pdf";

        // Verify input files exist
        if (!File.Exists(firstPdfPath) || !File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load the two PDF documents
        using (Document doc1 = new Document(firstPdfPath))
        using (Document doc2 = new Document(secondPdfPath))
        {
            // Instantiate the graphical comparer
            GraphicalPdfComparer comparer = new GraphicalPdfComparer();

            // Optional: customize appearance of differences
            // comparer.Color = Aspose.Pdf.Color.Red;
            // comparer.Resolution = 150;
            // comparer.Threshold = 0;

            // Generate a visual diff PDF
            comparer.CompareDocumentsToPdf(doc1, doc2, resultPdfPath);
        }

        Console.WriteLine($"Visual diff PDF created at '{resultPdfPath}'.");
    }
}