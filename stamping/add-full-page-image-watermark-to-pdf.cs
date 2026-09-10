using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "watermarked.pdf";
        const string watermarkImage = "watermark.png";

        if (!File.Exists(inputPdf) || !File.Exists(watermarkImage))
        {
            Console.Error.WriteLine("Input PDF or watermark image not found.");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Apply the watermark to every page
            foreach (Page page in doc.Pages)
            {
                // Create an ImageStamp from the watermark image
                ImageStamp stamp = new ImageStamp(watermarkImage)
                {
                    // Place the stamp behind page content
                    Background = true,
                    // Light opacity for a typical watermark effect
                    Opacity = 0.3f,
                    // Size the stamp to cover the whole page
                    Width  = page.Rect.Width,
                    Height = page.Rect.Height,
                    // Center the stamp on the page
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment   = VerticalAlignment.Center
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