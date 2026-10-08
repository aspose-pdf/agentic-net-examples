using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf; // Use the core Aspose.Pdf namespace for page manipulation

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string customPath = "custom_sized.pdf";
        const string revertedPath = "reverted.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // -----------------------------------------------------------------
        // Step 1: Load the PDF and record original page sizes
        // -----------------------------------------------------------------
        Document doc = new Document(inputPath);
        int pageCount = doc.Pages.Count;
        // Store width/height (in points) for each page so we can restore later
        List<(double Width, double Height)> originalSizes = new List<(double, double)>(pageCount);

        for (int i = 1; i <= pageCount; i++)
        {
            var pageInfo = doc.Pages[i].PageInfo;
            originalSizes.Add((pageInfo.Width, pageInfo.Height));
        }

        // -----------------------------------------------------------------
        // Step 2: Change each page to a custom size (e.g., 400 x 600 points)
        // -----------------------------------------------------------------
        const double customWidth = 400;   // points (1 point = 1/72 inch)
        const double customHeight = 600;

        for (int i = 1; i <= pageCount; i++)
        {
            var pageInfo = doc.Pages[i].PageInfo;
            pageInfo.Width = customWidth;
            pageInfo.Height = customHeight;
        }

        // Save the PDF with custom page dimensions
        doc.Save(customPath);
        Console.WriteLine($"Custom-sized PDF saved to '{customPath}'.");

        // -----------------------------------------------------------------
        // Step 3: Revert pages back to their original dimensions
        // -----------------------------------------------------------------
        Document revertDoc = new Document(customPath);

        for (int i = 1; i <= pageCount; i++)
        {
            var (origWidth, origHeight) = originalSizes[i - 1];
            var pageInfo = revertDoc.Pages[i].PageInfo;
            pageInfo.Width = origWidth;
            pageInfo.Height = origHeight;
        }

        // Save the reverted PDF
        revertDoc.Save(revertedPath);
        Console.WriteLine($"Reverted PDF saved to '{revertedPath}'.");
    }
}
