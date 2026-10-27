using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "watermarked.pdf";
        const string watermarkImagePath = "watermark.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }

        if (!File.Exists(watermarkImagePath))
        {
            Console.Error.WriteLine($"Watermark image not found: {watermarkImagePath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Load the watermark image using System.Drawing to obtain its dimensions.
            double imgWidth;
            double imgHeight;
            using (var bitmap = new Bitmap(watermarkImagePath))
            {
                // Aspose.Pdf works with points (1 point = 1/72 inch). Assuming the image DPI is 72,
                // the pixel dimensions can be used directly as points.
                imgWidth = bitmap.Width;
                imgHeight = bitmap.Height;
            }

            // Iterate pages using 1‑based indexing (Aspose.Pdf requirement).
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Page dimensions (points).
                double pageWidth = page.PageInfo.Width;
                double pageHeight = page.PageInfo.Height;

                // Grid step – you can add spacing if desired.
                double stepX = imgWidth;
                double stepY = imgHeight;

                // Place the watermark image repeatedly across the page.
                for (double y = 0; y < pageHeight; y += stepY)
                {
                    for (double x = 0; x < pageWidth; x += stepX)
                    {
                        // Create a new ImageStamp for each grid cell.
                        ImageStamp stamp = new ImageStamp(watermarkImagePath)
                        {
                            // Make the stamp semi‑transparent.
                            Opacity = 0.2,
                            // Ensure the stamp is drawn over existing content.
                            Background = false,
                            // Position using margins from the page origin (bottom‑left).
                            LeftMargin = x,
                            TopMargin = y
                        };

                        // Add the stamp to the current page.
                        page.AddStamp(stamp);
                    }
                }
            }

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}
