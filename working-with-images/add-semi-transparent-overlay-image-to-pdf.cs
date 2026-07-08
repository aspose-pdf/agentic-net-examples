using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";      // source PDF
        const string overlayImg = "overlay.png";    // semi‑transparent overlay image
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

        // Load the PDF document (lifecycle: load)
        using (Document doc = new Document(inputPdf))
        {
            // Create an ImageStamp that will be used as the overlay.
            // Background = true places the stamp behind existing page content.
            // Opacity controls the transparency (0.0 = fully transparent, 1.0 = opaque).
            ImageStamp stamp = new ImageStamp(overlayImg)
            {
                Background = true,
                Opacity    = 0.5f // 50 % transparency
            };

            // Size the stamp to cover the entire page.
            // Assuming all pages have the same size, use the first page as reference.
            Page firstPage = doc.Pages[1];
            stamp.Width  = firstPage.PageInfo.Width;
            stamp.Height = firstPage.PageInfo.Height;
            stamp.XIndent = 0;
            stamp.YIndent = 0;

            // Apply the stamp to every page (must call AddStamp per page).
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(stamp);
            }

            // Save the modified PDF (lifecycle: save)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Overlay applied and saved to '{outputPdf}'.");
    }
}