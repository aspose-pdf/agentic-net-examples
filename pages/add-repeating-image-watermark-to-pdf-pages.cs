using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string watermarkImage = "watermark.png"; // image to repeat
        const string outputPdf = "output_watermarked.pdf";

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

        // Load the PDF (using statement ensures proper disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Iterate over all pages
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Page dimensions (points)
                double pageWidth  = page.PageInfo.Width;
                double pageHeight = page.PageInfo.Height;

                // Create a prototype stamp to obtain its natural size
                ImageStamp prototype = new ImageStamp(watermarkImage);
                // Optional: scale the watermark (e.g., 50% of original)
                prototype.Width  = prototype.Width * 0.5;
                prototype.Height = prototype.Height * 0.5;
                prototype.Opacity = 0.3;          // semi‑transparent
                prototype.Background = true;      // place behind page content

                double stampWidth  = prototype.Width;
                double stampHeight = prototype.Height;

                // Define spacing between repeated watermarks
                double hSpacing = 50; // horizontal gap
                double vSpacing = 50; // vertical gap

                // Grid placement: start from bottom‑left corner
                for (double y = 0; y < pageHeight; y += stampHeight + vSpacing)
                {
                    for (double x = 0; x < pageWidth; x += stampWidth + hSpacing)
                    {
                        // Create a fresh stamp for each position
                        ImageStamp stamp = new ImageStamp(watermarkImage)
                        {
                            Width      = stampWidth,
                            Height     = stampHeight,
                            Opacity    = 0.3,
                            Background = true,
                            XIndent    = x,
                            YIndent    = y
                        };

                        // Add the stamp to the current page
                        page.AddStamp(stamp);
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPdf}'.");
    }
}