using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string overlayImg = "overlay.png";       // semi‑transparent overlay image
        const string outputPdf = "output.pdf";         // result PDF

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

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Create an ImageStamp that will be applied to each page
            ImageStamp stamp = new ImageStamp(overlayImg)
            {
                // Semi‑transparent (0.0 = fully transparent, 1.0 = opaque)
                Opacity = 0.3f,
                // Place the stamp on top of existing content
                Background = false,
                // Stretch the image to cover the whole page
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center
            };

            // Apply the stamp to every page
            foreach (Page page in doc.Pages)
            {
                // Adjust stamp size to match the current page dimensions
                stamp.Width  = page.PageInfo.Width;
                stamp.Height = page.PageInfo.Height;

                // Add the stamp to the page
                page.AddStamp(stamp);
            }

            // Save the modified PDF (lifecycle rule: use Save with path)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Overlay applied and saved to '{outputPdf}'.");
    }
}