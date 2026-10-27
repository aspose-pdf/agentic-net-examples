using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF and stamp image paths
        const string inputPdfPath  = "input.pdf";
        const string stampImgPath  = "stamp.png";
        const string outputPdfPath = "output.pdf";

        // Percentage offsets (e.g., 10% from left, 20% from bottom)
        const double offsetPercentX = 0.10; // 10% of page width
        const double offsetPercentY = 0.20; // 20% of page height

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(stampImgPath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImgPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                Page page = pdfDoc.Pages[pageNum];

                // Compute absolute offsets based on page dimensions
                double xOffset = page.PageInfo.Width  * offsetPercentX;
                double yOffset = page.PageInfo.Height * offsetPercentY;

                // Create the image stamp
                ImageStamp imgStamp = new ImageStamp(stampImgPath)
                {
                    // Use absolute positioning; disable alignment so XIndent/YIndent are respected
                    HorizontalAlignment = HorizontalAlignment.None,
                    VerticalAlignment   = VerticalAlignment.None,
                    XIndent = xOffset,
                    YIndent = yOffset
                };

                // Apply the stamp to the current page
                page.AddStamp(imgStamp);
            }

            // Save the modified PDF (no SaveOptions needed for PDF output)
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Image stamp applied with percentage offsets and saved to '{outputPdfPath}'.");
    }
}