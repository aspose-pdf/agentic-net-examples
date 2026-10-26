using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";      // source PDF
        const string overlayImg = "overlay.png";    // semi‑transparent image to overlay
        const string outputPdf  = "output.pdf";     // result PDF

        // Verify required files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(overlayImg))
        {
            Console.Error.WriteLine($"Overlay image not found: {overlayImg}");
            return;
        }

        // Load the PDF inside a using block (document‑disposal‑with‑using rule)
        using (Document doc = new Document(inputPdf))
        {
            // Create an ImageStamp that will be placed on every page
            ImageStamp stamp = new ImageStamp(overlayImg)
            {
                // Place on top of existing content
                Background = false,
                // Semi‑transparent (0 = fully transparent, 1 = opaque)
                Opacity = 0.2,
                // Center the image; we will stretch it to page size below
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center
            };

            // Iterate pages using 1‑based indexing (page-indexing-one-based rule)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Stretch the stamp to cover the whole page
                stamp.Width  = page.PageInfo.Width;
                stamp.Height = page.PageInfo.Height;

                // Apply the stamp to the current page
                page.AddStamp(stamp);
            }

            // Save the modified PDF (PDF format, no special SaveOptions needed)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Overlay applied and saved to '{outputPdf}'.");
    }
}