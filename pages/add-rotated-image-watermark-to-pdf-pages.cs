using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // for ImageStamp

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "watermarked.pdf";
        const string imagePath = "watermark.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Watermark image not found: {imagePath}");
            return;
        }

        // Load the PDF document (lifecycle: load)
        using (Document doc = new Document(inputPdf))
        {
            // Apply the watermark to each page
            foreach (Page page in doc.Pages)
            {
                // Create an ImageStamp from the image file
                ImageStamp stamp = new ImageStamp(imagePath);

                // Place the stamp over the page content
                stamp.Background = false;                     // overlay (not background)
                stamp.Opacity    = 1.0;                       // fully opaque
                stamp.RotateAngle = 45;                       // rotate 45 degrees
                stamp.Zoom        = 0.5;                      // scale to 50% of original size
                stamp.HorizontalAlignment = HorizontalAlignment.Center;
                stamp.VerticalAlignment   = VerticalAlignment.Center;

                // Add the stamp to the current page (lifecycle: modify)
                page.AddStamp(stamp);
            }

            // Save the modified PDF (lifecycle: save)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPdf}'.");
    }
}