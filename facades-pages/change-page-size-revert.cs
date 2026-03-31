using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string customSizePath = "custom_size.pdf";
        const string revertedPath = "reverted_size.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the original PDF document
        using (Document doc = new Document(inputPath))
        {
            // Work with the first page (pages are 1‑based)
            Page page = doc.Pages[1];

            // Store original dimensions
            double originalWidth = page.PageInfo.Width;
            double originalHeight = page.PageInfo.Height;

            // Set a custom page size (example: 420 x 595 points)
            double customWidth = 420.0;
            double customHeight = 595.0;
            page.SetPageSize(customWidth, customHeight);
            doc.Save(customSizePath);
            Console.WriteLine($"Custom size saved to '{customSizePath}' (Width={customWidth}, Height={customHeight})");

            // Revert to the original size
            page.SetPageSize(originalWidth, originalHeight);
            doc.Save(revertedPath);
            Console.WriteLine($"Reverted size saved to '{revertedPath}' (Width={originalWidth}, Height={originalHeight})");
        }
    }
}
