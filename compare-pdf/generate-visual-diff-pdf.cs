using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string firstPdf  = "document1.pdf";
        const string secondPdf = "document2.pdf";
        const string resultPdf = "visual_diff.pdf";

        // Verify input files exist
        if (!File.Exists(firstPdf) || !File.Exists(secondPdf))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        try
        {
            // Load both PDFs inside using blocks for deterministic disposal
            using (Document doc1 = new Document(firstPdf))
            using (Document doc2 = new Document(secondPdf))
            {
                // Create the comparer and generate a visual diff PDF
                GraphicalPdfComparer comparer = new GraphicalPdfComparer();
                comparer.CompareDocumentsToPdf(doc1, doc2, resultPdf);
            }

            Console.WriteLine($"Visual diff PDF created at '{resultPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during comparison: {ex.Message}");
        }
    }
}