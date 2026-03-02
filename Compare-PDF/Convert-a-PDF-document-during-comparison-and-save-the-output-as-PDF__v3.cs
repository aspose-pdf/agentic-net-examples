using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        // Paths to the source PDF files and the result PDF.
        const string pdfPath1 = "document1.pdf";
        const string pdfPath2 = "document2.pdf";
        const string resultPdfPath = "comparison_result.pdf";

        // Verify that both input files exist.
        if (!File.Exists(pdfPath1) || !File.Exists(pdfPath2))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        try
        {
            // Load the two PDF documents inside using blocks for deterministic disposal.
            using (Document doc1 = new Document(pdfPath1))
            using (Document doc2 = new Document(pdfPath2))
            {
                // Create the graphical comparer and perform the comparison.
                GraphicalPdfComparer comparer = new GraphicalPdfComparer();
                comparer.CompareDocumentsToPdf(doc1, doc2, resultPdfPath);
            }

            Console.WriteLine($"Comparison PDF saved to '{resultPdfPath}'.");
        }
        catch (Exception ex)
        {
            // Handle any errors that may occur during loading, comparison, or saving.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}