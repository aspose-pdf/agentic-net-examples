using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "output_with_background.pdf";
        const string backgroundImgPath = "background.png";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(backgroundImgPath))
        {
            Console.Error.WriteLine($"Background image not found: {backgroundImgPath}");
            return;
        }

        // Load the source PDF inside a using block (document-disposal-with-using rule)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create a reusable ImageStamp that will be applied to every page
            ImageStamp bgStamp = new ImageStamp(backgroundImgPath)
            {
                // Place the image behind the page content
                Background = true,
                // Stretch the image to cover the whole page (optional)
                // Adjust Width/Height if needed; here we use page dimensions later
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center,
                // Set opacity if a translucent background is desired
                Opacity = 1.0
            };

            // Iterate pages using 1‑based indexing (page-indexing-one-based rule)
            for (int i = 1; i <= pdfDoc.Pages.Count; i++)
            {
                Page page = pdfDoc.Pages[i];

                // Adjust stamp size to match the page dimensions
                bgStamp.Width  = page.PageInfo.Width;
                bgStamp.Height = page.PageInfo.Height;

                // Apply the background stamp to the current page (add-stamp-per-page rule)
                page.AddStamp(bgStamp);
            }

            // Save the modified PDF (save-to-non-pdf-always-use-save-options not needed here)
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Background image added to all pages. Saved as '{outputPdfPath}'.");
    }
}