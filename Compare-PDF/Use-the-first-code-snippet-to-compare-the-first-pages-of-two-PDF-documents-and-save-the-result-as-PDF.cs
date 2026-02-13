using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class CompareFirstPages
{
    static void Main(string[] args)
    {
        // Input PDF file paths
        const string pdfPath1 = "input1.pdf";
        const string pdfPath2 = "input2.pdf";
        // Output PDF file path
        const string outputPath = "comparison.pdf";

        // Verify that both source files exist
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

        // Load the two source documents
        Document doc1 = new Document(pdfPath1);
        Document doc2 = new Document(pdfPath2);

        // Get the first page of each document (Aspose.Pdf collections are 1‑based)
        Page firstPage1 = doc1.Pages[1];
        Page firstPage2 = doc2.Pages[1];

        // Create a new document that will hold the side‑by‑side comparison
        Document resultDoc = new Document();
        // Add a blank page to the result document
        Page resultPage = resultDoc.Pages.Add();

        // Determine half of the result page width for side‑by‑side layout
        double halfWidth = resultPage.PageInfo.Width / 2.0;
        double pageHeight = resultPage.PageInfo.Height;

        // Stamp the first source page on the left half
        PdfPageStamp stamp1 = new PdfPageStamp(firstPage1);
        stamp1.XIndent = 0;               // left edge
        stamp1.YIndent = 0;               // bottom edge
        stamp1.Width = halfWidth;         // occupy left half
        stamp1.Height = pageHeight;       // full height
        resultPage.AddStamp(stamp1);

        // Stamp the second source page on the right half
        PdfPageStamp stamp2 = new PdfPageStamp(firstPage2);
        stamp2.XIndent = halfWidth;       // start at middle of the page
        stamp2.YIndent = 0;
        stamp2.Width = halfWidth;         // occupy right half
        stamp2.Height = pageHeight;
        resultPage.AddStamp(stamp2);

        // Save the resulting comparison PDF
        resultDoc.Save(outputPath);
        Console.WriteLine($"Comparison PDF saved to: {outputPath}");
    }
}