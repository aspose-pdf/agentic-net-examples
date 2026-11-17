using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string stampImagePath = "stamp.png";
        const string outputPdfPath  = "output.pdf";

        // Percentage offsets relative to page size
        const double offsetXPercent = 10.0; // 10 % from the left edge
        const double offsetYPercent = 5.0;  // 5 % from the bottom edge

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(stampImagePath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImagePath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Create a single ImageStamp instance (reused for each page)
            ImageStamp stamp = new ImageStamp(stampImagePath)
            {
                Background = false,                         // overlay the stamp
                HorizontalAlignment = HorizontalAlignment.None, // we'll use XIndent
                VerticalAlignment   = VerticalAlignment.None    // we'll use YIndent
            };

            // Iterate pages using 1‑based indexing (Aspose.Pdf requirement)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Compute absolute offsets based on page dimensions (points)
                double xOffset = page.PageInfo.Width  * offsetXPercent / 100.0;
                double yOffset = page.PageInfo.Height * offsetYPercent / 100.0;

                // Apply the calculated offsets to the stamp
                stamp.XIndent = xOffset;
                stamp.YIndent = yOffset;

                // Add the stamp to the current page
                page.AddStamp(stamp);
            }

            // Save the modified PDF (PDF format, no special SaveOptions needed)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPdfPath}'.");
    }
}