using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputPdf = "watermarked.pdf";    // result PDF
        const string watermarkImage = "logo.png";      // image to repeat

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        if (!File.Exists(watermarkImage))
        {
            Console.Error.WriteLine($"Watermark image not found: {watermarkImage}");
            return;
        }

        // Load the PDF document (using rule: wrap in using for deterministic disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Iterate over all pages
            foreach (Page page in doc.Pages)
            {
                // Page dimensions (points)
                double pageWidth  = page.PageInfo.Width;
                double pageHeight = page.PageInfo.Height;

                // Load the image once to obtain its size
                ImageStamp sampleStamp = new ImageStamp(watermarkImage);
                double imgWidth  = sampleStamp.Width  > 0 ? sampleStamp.Width  : 100; // fallback width
                double imgHeight = sampleStamp.Height > 0 ? sampleStamp.Height : 100; // fallback height

                // Define spacing between repeated watermarks
                double hSpacing = imgWidth  + 50; // horizontal gap
                double vSpacing = imgHeight + 50; // vertical gap

                // Loop to place stamps in a grid
                for (double y = 0; y < pageHeight; y += vSpacing)
                {
                    for (double x = 0; x < pageWidth; x += hSpacing)
                    {
                        // Create a new stamp for each position
                        ImageStamp stamp = new ImageStamp(watermarkImage)
                        {
                            // Place the stamp at (x, y) from the bottom‑left corner
                            XIndent = x,
                            YIndent = y,
                            // Draw behind page content
                            Background = true,
                            // Optional: make the watermark semi‑transparent
                            Opacity = 0.3f
                        };

                        // Add the stamp to the current page
                        page.AddStamp(stamp);
                    }
                }
            }

            // Save the modified PDF (using rule: Document.Save)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPdf}'.");
    }
}