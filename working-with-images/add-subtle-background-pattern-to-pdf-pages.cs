using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "output.pdf";
        const string patternImgPath = "pattern.png";

        // Verify required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(patternImgPath))
        {
            Console.Error.WriteLine($"Pattern image not found: {patternImgPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Iterate over all pages (foreach respects 1‑based indexing internally)
            foreach (Page page in pdfDoc.Pages)
            {
                // Create an ImageStamp for the background pattern
                ImageStamp bgStamp = new ImageStamp(patternImgPath)
                {
                    // Place the stamp behind existing page content
                    Background = true,
                    // Set opacity to 5 percent for a subtle effect
                    Opacity = 0.05f,
                    // Scale the image to cover the entire page
                    Width = page.PageInfo.Width,
                    Height = page.PageInfo.Height,
                    // Ensure the image starts at the page origin
                    LeftMargin = 0,
                    BottomMargin = 0,
                    TopMargin = 0,
                    RightMargin = 0
                };

                // Apply the stamp to the current page
                page.AddStamp(bgStamp);
            }

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Background pattern applied and saved to '{outputPdfPath}'.");
    }
}
