using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string docPath1 = "doc1.pdf";
        const string docPath2 = "doc2.pdf";
        const string resultPath = "comparison_result.pdf";

        if (!File.Exists(docPath1))
        {
            Console.Error.WriteLine($"File not found: {docPath1}");
            return;
        }
        if (!File.Exists(docPath2))
        {
            Console.Error.WriteLine($"File not found: {docPath2}");
            return;
        }

        try
        {
            // Load both PDFs inside using blocks for deterministic disposal
            using (Document doc1 = new Document(docPath1))
            using (Document doc2 = new Document(docPath2))
            {
                // Create the graphical comparer
                GraphicalPdfComparer comparer = new GraphicalPdfComparer();

                // Optional: customize appearance
                // comparer.Color = Aspose.Pdf.Color.Red;
                // comparer.Resolution = 150;
                // comparer.Threshold = 0;

                // Perform comparison and save the result as a PDF
                comparer.CompareDocumentsToPdf(doc1, doc2, resultPath);
            }

            Console.WriteLine($"Comparison PDF saved to '{resultPath}'.");
        }
        catch (ArgumentException ex)
        {
            Console.Error.WriteLine($"Argument error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}