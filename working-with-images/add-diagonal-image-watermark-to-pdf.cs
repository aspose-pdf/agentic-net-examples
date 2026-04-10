using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // for ImageStamp (inherits from Stamp)

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string watermarkImage = "watermark.png";
        const string outputPdf = "output_watermarked.pdf";

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
            // Create an ImageStamp with the watermark image
            ImageStamp stamp = new ImageStamp(watermarkImage);

            // Set the stamp to appear over the page content (false = foreground)
            stamp.Background = false;

            // Optional: make the watermark semi‑transparent
            stamp.Opacity = 0.3;

            // Center the stamp on each page
            stamp.HorizontalAlignment = HorizontalAlignment.Center;
            stamp.VerticalAlignment   = VerticalAlignment.Center;

            // Rotate the stamp by 45 degrees for diagonal placement
            stamp.RotateAngle = 45;

            // Apply the stamp to every page in the document
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(stamp);
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPdf}'.");
    }
}