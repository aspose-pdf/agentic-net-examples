using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string watermarkPath  = "watermark.png";
        const string outputPdfPath  = "watermarked_output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(watermarkPath))
        {
            Console.Error.WriteLine($"Watermark image not found: {watermarkPath}");
            return;
        }

        // Open the source PDF inside a using block for deterministic disposal
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPdfPath))
        {
            // Iterate over all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Aspose.Pdf.Page page = doc.Pages[i];

                // Create an ImageStamp for the watermark image
                Aspose.Pdf.ImageStamp stamp = new Aspose.Pdf.ImageStamp(watermarkPath)
                {
                    // Do not place the stamp behind the page content
                    Background = false,

                    // 50 % opacity makes the watermark semi‑transparent
                    Opacity = 0.5,

                    // Rotate the image 45 degrees
                    RotateAngle = 45,

                    // Scale to half of the page size
                    Width  = page.PageInfo.Width  / 2,
                    Height = page.PageInfo.Height / 2,

                    // Center the stamp on the page
                    HorizontalAlignment = Aspose.Pdf.HorizontalAlignment.Center,
                    VerticalAlignment   = Aspose.Pdf.VerticalAlignment.Center
                };

                // Apply the stamp to the current page
                page.AddStamp(stamp);
            }

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPdfPath}'.");
    }
}