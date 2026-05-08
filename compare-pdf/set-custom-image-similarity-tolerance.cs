using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        // Paths to the two PDF files to compare
        const string pdfPath1 = "document1.pdf";
        const string pdfPath2 = "document2.pdf";
        // Path where the comparison result will be saved
        const string resultPath = "comparison_result.pdf";

        // Verify that both source files exist
        if (!File.Exists(pdfPath1) || !File.Exists(pdfPath2))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load the PDFs using the core Document class (no Facades)
        using (Document doc1 = new Document(pdfPath1))
        using (Document doc2 = new Document(pdfPath2))
        {
            // Create the graphical comparer – suitable for scanned-image PDFs
            GraphicalPdfComparer comparer = new GraphicalPdfComparer();

            // Set a custom tolerance (percentage) for image similarity.
            // Differences below this threshold will be ignored.
            comparer.Threshold = 5.0; // 5 % tolerance

            // Perform the comparison and output the result as a PDF.
            comparer.CompareDocumentsToPdf(doc1, doc2, resultPath);
        }

        Console.WriteLine($"Comparison completed. Result saved to '{resultPath}'.");
    }
}