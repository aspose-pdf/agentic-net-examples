using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string pdfPath1   = "first.pdf";
        const string pdfPath2   = "second.pdf";
        const string resultPath = "first_pages_comparison.pdf";

        // Verify input files exist
        if (!File.Exists(pdfPath1))
        {
            Console.Error.WriteLine($"File not found: {pdfPath1}");
            return;
        }
        if (!File.Exists(pdfPath2))
        {
            Console.Error.WriteLine($"File not found: {pdfPath2}");
            return;
        }

        // Load both PDFs inside using blocks (deterministic disposal)
        using (Document doc1 = new Document(pdfPath1))
        using (Document doc2 = new Document(pdfPath2))
        {
            // Aspose.Pdf uses 1‑based page indexing
            if (doc1.Pages.Count == 0 || doc2.Pages.Count == 0)
            {
                Console.Error.WriteLine("One of the documents has no pages.");
                return;
            }

            Page firstPageDoc1 = doc1.Pages[1];
            Page firstPageDoc2 = doc2.Pages[1];

            // Perform graphical comparison of the first pages
            GraphicalPdfComparer comparer = new GraphicalPdfComparer();
            comparer.ComparePagesToPdf(firstPageDoc1, firstPageDoc2, resultPath);
        }

        Console.WriteLine($"Comparison PDF saved to '{resultPath}'.");
    }
}