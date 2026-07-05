using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // source PDF
        const string outputPdf  = "watermarked.pdf";    // result PDF
        const string watermarkImage = "watermark.png";   // image to use as full‑page watermark

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(watermarkImage))
        {
            Console.Error.WriteLine($"Watermark image not found: {watermarkImage}");
            return;
        }

        // Load the PDF document (lifecycle rule: wrap in using)
        using (Document doc = new Document(inputPdf))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create an ImageStamp that uses the whole page area
                ImageStamp imgStamp = new ImageStamp(watermarkImage)
                {
                    // Make the stamp a background element (behind page content)
                    Background = true,

                    // Set opacity so the underlying content remains readable
                    Opacity = 0.3f,

                    // Align the stamp to the page and size it to cover the page
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment   = VerticalAlignment.Center,
                    Width  = page.Rect.Width,
                    Height = page.Rect.Height
                };

                // Add the stamp to the current page
                page.AddStamp(imgStamp);
            }

            // Save the modified document (lifecycle rule: save inside using)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPdf}'.");
    }
}