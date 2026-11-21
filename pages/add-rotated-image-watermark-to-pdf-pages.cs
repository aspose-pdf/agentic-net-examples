using System;
using System.IO;
using Aspose.Pdf;

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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate pages using 1‑based indexing (Aspose.Pdf uses 1‑based page numbers)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Use PageInfo.Width / Height to obtain page dimensions
                double pageWidth = page.PageInfo.Width;
                double pageHeight = page.PageInfo.Height;

                // Create an ImageStamp for the watermark
                ImageStamp stamp = new ImageStamp(watermarkImagePath)
                {
                    // Scale to half the page size
                    Width = pageWidth / 2,
                    Height = pageHeight / 2,

                    // Center the stamp on the page using XIndent/YIndent
                    XIndent = (pageWidth - (pageWidth / 2)) / 2,
                    YIndent = (pageHeight - (pageHeight / 2)) / 2,

                    // Rotate 45 degrees
                    RotateAngle = 45,

                    // Place behind existing content
                    Background = true,

                    // Semi‑transparent watermark
                    Opacity = 0.5
                };

                // Add the stamp to the current page
                page.AddStamp(stamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}
