using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string pdfPath1 = "first.pdf";
        const string pdfPath2 = "second.pdf";
        const string resultPdfPath = "comparison_result.pdf";

        // Verify input files exist
        if (!File.Exists(pdfPath1) || !File.Exists(pdfPath2))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        try
        {
            // Load both PDF documents inside using blocks for deterministic disposal
            using (Document doc1 = new Document(pdfPath1))
            using (Document doc2 = new Document(pdfPath2))
            {
                // Ensure each document has at least one page (pages are 1‑based)
                if (doc1.Pages.Count == 0 || doc2.Pages.Count == 0)
                {
                    Console.Error.WriteLine("One of the documents does not contain any pages.");
                    return;
                }

                // Retrieve the first page from each document
                Page firstPageDoc1 = doc1.Pages[1];
                Page firstPageDoc2 = doc2.Pages[1];

                // Perform graphical comparison of the two pages and save the result as PDF
                GraphicalPdfComparer comparer = new GraphicalPdfComparer();
                comparer.ComparePagesToPdf(firstPageDoc1, firstPageDoc2, resultPdfPath);
            }

            Console.WriteLine($"Comparison PDF saved to '{resultPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during comparison: {ex.Message}");
        }
    }
}