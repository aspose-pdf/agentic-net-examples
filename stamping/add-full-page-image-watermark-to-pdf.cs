using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";
        const string outputPdf  = "watermarked.pdf";
        const string watermarkImage = "watermark.png";

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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create an ImageStamp that uses the whole page area
                ImageStamp stamp = new ImageStamp(watermarkImage)
                {
                    // Place the stamp behind the page content
                    Background = true,

                    // Make the stamp semi‑transparent (0.0 = fully transparent, 1.0 = opaque)
                    Opacity = 0.3f,

                    // Align the stamp to cover the entire page
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment   = VerticalAlignment.Center,

                    // Desired size – match the page dimensions
                    Width  = page.PageInfo.Width,
                    Height = page.PageInfo.Height
                };

                // Add the stamp to the current page
                page.AddStamp(stamp);
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPdf}'.");
    }
}