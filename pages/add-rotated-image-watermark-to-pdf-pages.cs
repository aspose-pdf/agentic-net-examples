using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string watermarkImage = "watermark.png";
        const string outputPdf = "output.pdf";

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

        // Load the PDF document (using block ensures proper disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Iterate over each page (Aspose.Pdf uses 1‑based indexing)
            foreach (Page page in doc.Pages)
            {
                // Create an ImageStamp for the watermark image
                ImageStamp stamp = new ImageStamp(watermarkImage);

                // Place the stamp behind the page content (optional)
                stamp.Background = false;

                // Set opacity (0.0 = fully transparent, 1.0 = fully opaque)
                stamp.Opacity = 0.5f;

                // Rotate the stamp by 45 degrees (arbitrary angle)
                stamp.RotateAngle = 45;

                // Scale the stamp to half the page size
                stamp.Width  = page.Rect.Width  / 2;
                stamp.Height = page.Rect.Height / 2;

                // Center the stamp on the page
                stamp.HorizontalAlignment = HorizontalAlignment.Center;
                stamp.VerticalAlignment   = VerticalAlignment.Center;

                // Add the stamp to the current page
                page.AddStamp(stamp);
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPdf}'.");
    }
}