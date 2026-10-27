using System;
using System.IO;
using Aspose.Pdf;

class ReplaceBackgroundImage
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string highResImgPath = "highres_background.png";
        const string outputPdfPath  = "output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(highResImgPath))
        {
            Console.Error.WriteLine($"High‑resolution image not found: {highResImgPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                Page page = pdfDoc.Pages[pageNum];

                // Create an ImageStamp that will act as the page background.
                // Setting Background = true places the stamp behind existing page content.
                ImageStamp bgStamp = new ImageStamp(highResImgPath)
                {
                    Background = true,
                    // Stretch the image to cover the whole page without altering layout.
                    Width  = page.PageInfo.Width,
                    Height = page.PageInfo.Height,
                    // Position at the lower‑left corner of the page.
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment   = VerticalAlignment.Bottom,
                    XIndent = 0,
                    YIndent = 0
                };

                // Add the new stamp; it will appear behind existing content and effectively replace any previous background.
                page.AddStamp(bgStamp);
            }

            // Save the modified PDF (Document.Save writes PDF regardless of extension)
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Background images replaced. Output saved to '{outputPdfPath}'.");
    }
}