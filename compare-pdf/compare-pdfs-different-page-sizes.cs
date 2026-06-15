using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class PdfComparisonDemo
{
    static void Main()
    {
        const string pdfPath1 = "documentA.pdf";
        const string pdfPath2 = "documentB.pdf";
        const string resultPath = "comparison_result.pdf";

        if (!File.Exists(pdfPath1) || !File.Exists(pdfPath2))
        {
            Console.Error.WriteLine("One or both input PDF files are missing.");
            return;
        }

        // Load both PDF documents inside using blocks for deterministic disposal
        using (Document doc1 = new Document(pdfPath1))
        using (Document doc2 = new Document(pdfPath2))
        {
            // Retrieve the first pages (Aspose.Pdf uses 1‑based indexing)
            Page page1 = doc1.Pages[1];
            Page page2 = doc2.Pages[1];

            // Ensure both pages have identical dimensions before comparison.
            // If they differ, resize the second page to match the first page.
            if (page1.PageInfo.Width != page2.PageInfo.Width ||
                page1.PageInfo.Height != page2.PageInfo.Height)
            {
                // Adjust the size of page2 to match page1 using PageInfo properties.
                page2.PageInfo.Width = page1.PageInfo.Width;
                page2.PageInfo.Height = page1.PageInfo.Height;
                page2.PageInfo.IsLandscape = page1.PageInfo.IsLandscape;
            }

            // Create the graphical comparer.
            GraphicalPdfComparer comparer = new GraphicalPdfComparer();

            // Optional: customize the highlight color for differences.
            comparer.Color = Aspose.Pdf.Color.Red;

            // Perform the comparison and write the result to a PDF file.
            comparer.ComparePagesToPdf(page1, page2, resultPath);
        }

        Console.WriteLine($"Comparison completed. Result saved to '{resultPath}'.");
    }
}
