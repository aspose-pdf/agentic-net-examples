using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class PdfComparisonDemo
{
    static void Main()
    {
        // Input PDF files (different page sizes)
        const string pdfPath1 = "documentA.pdf";
        const string pdfPath2 = "documentB.pdf";

        // Output comparison PDF (pages aligned and compared)
        const string resultPath = "comparisonResult.pdf";

        // Verify input files exist
        if (!File.Exists(pdfPath1) || !File.Exists(pdfPath2))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load both documents inside using blocks for deterministic disposal
        using (Document doc1 = new Document(pdfPath1))
        using (Document doc2 = new Document(pdfPath2))
        {
            // For demonstration we compare the first page of each document.
            // If the documents have multiple pages, you could iterate over them similarly.
            Page page1 = doc1.Pages[1];
            Page page2 = doc2.Pages[1];

            // Align page sizes: make page2 the same size as page1.
            // PageInfo.Width and Height are mutable and affect the page dimensions.
            page2.PageInfo.Width = page1.PageInfo.Width;
            page2.PageInfo.Height = page1.PageInfo.Height;

            // Optional: adjust the MediaBox/TrimBox if needed (usually PageInfo is sufficient)
            // page2.MediaBox = page1.MediaBox;

            // Create the graphical comparer
            GraphicalPdfComparer comparer = new GraphicalPdfComparer
            {
                // Example customizations (default values are fine)
                // Color = Aspose.Pdf.Color.Red;
                // Resolution = 150;
                // Threshold = 0;
            };

            // Perform the comparison and write the result to a PDF file.
            // This method throws ArgumentException if page sizes differ, but we have aligned them.
            comparer.ComparePagesToPdf(page1, page2, resultPath);
        }

        Console.WriteLine($"Comparison completed. Result saved to '{resultPath}'.");
    }
}