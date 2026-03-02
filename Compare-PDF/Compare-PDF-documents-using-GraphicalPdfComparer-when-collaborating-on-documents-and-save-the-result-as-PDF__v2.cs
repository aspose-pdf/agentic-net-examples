using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string doc1Path = "doc1.pdf";
        const string doc2Path = "doc2.pdf";
        const string resultPath = "comparison_result.pdf";

        // Verify that both source PDFs exist
        if (!File.Exists(doc1Path) || !File.Exists(doc2Path))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        try
        {
            // Load the two documents inside using blocks for deterministic disposal
            using (Document doc1 = new Document(doc1Path))
            using (Document doc2 = new Document(doc2Path))
            {
                // Create the comparer instance
                GraphicalPdfComparer comparer = new GraphicalPdfComparer();

                // Optional: customize comparison appearance
                // comparer.Color = Aspose.Pdf.Color.Red;
                // comparer.Threshold = 5; // ignore changes smaller than 5%
                // comparer.Resolution = 200; // DPI for internal rendering

                // Perform the comparison and save the result as a PDF
                comparer.CompareDocumentsToPdf(doc1, doc2, resultPath);
            }

            Console.WriteLine($"Comparison PDF saved to '{resultPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during comparison: {ex.Message}");
        }
    }
}