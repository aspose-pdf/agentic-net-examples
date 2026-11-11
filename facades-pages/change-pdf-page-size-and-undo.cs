using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string customSizePath = "custom_size.pdf";
        const string revertedPath = "reverted.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the original document to capture the original page dimensions.
        double originalWidth, originalHeight;
        using (Document srcDoc = new Document(inputPath))
        {
            // Pages are 1‑based; retrieve the first page.
            Page firstPage = srcDoc.Pages[1];
            originalWidth = firstPage.PageInfo.Width;
            originalHeight = firstPage.PageInfo.Height;
        }

        // Define custom dimensions (example: 500 × 700 points).
        double customWidth = 500;
        double customHeight = 700;

        // -----------------------------------------------------------------
        // Change page size using the Document API (PdfPageEditor no longer
        // provides SetPageSizeByCropBox).
        // -----------------------------------------------------------------
        using (Document doc = new Document(inputPath))
        {
            Page page = doc.Pages[1];
            page.PageInfo.Width = customWidth;
            page.PageInfo.Height = customHeight;
            doc.Save(customSizePath);
        }

        // -----------------------------------------------------------------
        // Revert to the original page size to test undo functionality.
        // -----------------------------------------------------------------
        using (Document doc = new Document(customSizePath))
        {
            Page page = doc.Pages[1];
            page.PageInfo.Width = originalWidth;
            page.PageInfo.Height = originalHeight;
            doc.Save(revertedPath);
        }

        Console.WriteLine($"Custom‑sized PDF saved to '{customSizePath}'.");
        Console.WriteLine($"Reverted PDF saved to '{revertedPath}'.");
    }
}
