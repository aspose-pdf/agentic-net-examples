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
        const string diffPdfPath   = "visual_diff.pdf";

        // Verify input files exist
        if (!File.Exists(firstPdfPath) || !File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load both PDFs inside using blocks for deterministic disposal
        using (Document doc1 = new Document(firstPdfPath))
        using (Document doc2 = new Document(secondPdfPath))
        {
            // Instantiate the visual comparer
            GraphicalPdfComparer comparer = new GraphicalPdfComparer();

            // Generate the visual diff PDF using the correct API method
            comparer.CompareDocumentsToPdf(doc1, doc2, diffPdfPath);
        }

        Console.WriteLine($"Visual diff PDF created at '{diffPdfPath}'.");
    }
}
