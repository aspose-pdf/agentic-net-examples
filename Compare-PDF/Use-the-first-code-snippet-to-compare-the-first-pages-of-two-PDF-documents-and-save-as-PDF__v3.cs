using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison; // GraphicalPdfComparer resides here

class Program
{
    static void Main()
    {
        const string pdf1Path = "first.pdf";
        const string pdf2Path = "second.pdf";
        const string resultPath = "comparison_result.pdf";

        // Verify input files exist
        if (!File.Exists(pdf1Path) || !File.Exists(pdf2Path))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        try
        {
            // Load both documents inside using blocks for deterministic disposal
            using (Document doc1 = new Document(pdf1Path))
            using (Document doc2 = new Document(pdf2Path))
            {
                // Aspose.Pdf uses 1‑based page indexing
                Page page1 = doc1.Pages[1];
                Page page2 = doc2.Pages[1];

                // Perform graphical comparison of the first pages and save the result as PDF
                GraphicalPdfComparer comparer = new GraphicalPdfComparer();
                comparer.ComparePagesToPdf(page1, page2, resultPath);
            }

            Console.WriteLine($"Comparison PDF saved to '{resultPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during comparison: {ex.Message}");
        }
    }
}