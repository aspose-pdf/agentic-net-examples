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
        const string resultPath = "comparison_result.pdf";

        // Verify input files exist
        if (!File.Exists(pdfPath1) || !File.Exists(pdfPath2))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        try
        {
            // Load both documents
            using (Document doc1 = new Document(pdfPath1))
            using (Document doc2 = new Document(pdfPath2))
            {
                // Ensure both documents have at least one page
                if (doc1.Pages.Count == 0 || doc2.Pages.Count == 0)
                {
                    Console.Error.WriteLine("One of the PDFs does not contain any pages.");
                    return;
                }

                // Work with the first page of each document for this demo
                Page page1 = doc1.Pages[1];
                Page page2 = doc2.Pages[1];

                // Determine a common size (use the larger width and height)
                double commonWidth = Math.Max(page1.PageInfo.Width, page2.PageInfo.Width);
                double commonHeight = Math.Max(page1.PageInfo.Height, page2.PageInfo.Height);

                // Resize both pages to the common dimensions
                page1.PageInfo.Width = commonWidth;
                page1.PageInfo.Height = commonHeight;

                page2.PageInfo.Width = commonWidth;
                page2.PageInfo.Height = commonHeight;

                // Optional: center original content after resizing (preserve aspect)
                // Here we simply keep the content at its original position.
                // More sophisticated alignment can be done by translating the page contents.

                // Perform graphical comparison and save the result PDF
                GraphicalPdfComparer comparer = new GraphicalPdfComparer();
                comparer.ComparePagesToPdf(page1, page2, resultPath);

                Console.WriteLine($"Comparison completed. Result saved to '{resultPath}'.");
            }
        }
        catch (ArgumentException argEx)
        {
            // Thrown if pages still differ in size after adjustments or invalid arguments
            Console.Error.WriteLine($"Argument error: {argEx.Message}");
        }
        catch (Exception ex)
        {
            // General error handling
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}