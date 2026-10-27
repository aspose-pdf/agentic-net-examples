using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // source PDF
        const string outputPdf  = "output.pdf";         // result PDF
        const string patternImg = "pattern.png";        // background pattern image

        // Verify required files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(patternImg))
        {
            Console.Error.WriteLine($"Pattern image not found: {patternImg}");
            return;
        }

        // Load the PDF and ensure deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Iterate over all pages (PageCollection implements IEnumerable)
            foreach (Page page in doc.Pages)
            {
                // Create a stamp from the pattern image
                ImageStamp stamp = new ImageStamp(patternImg)
                {
                    // Place the stamp behind existing page content
                    Background = true,
                    // Set opacity to 10 percent for a subtle effect
                    Opacity = 0.1,
                    // Stretch the image to cover the whole page
                    Width  = page.PageInfo.Width,
                    Height = page.PageInfo.Height,
                    // Center alignment (required when width/height are set)
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment   = VerticalAlignment.Center
                };

                // Add the stamp to the current page
                page.AddStamp(stamp);
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Background pattern applied and saved to '{outputPdf}'.");
    }
}