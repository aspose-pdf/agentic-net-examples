using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string watermarkImage = "watermark.png";
        const string outputPdf = "watermarked.pdf";

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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Create an ImageStamp for the watermark
            ImageStamp stamp = new ImageStamp(watermarkImage)
            {
                // Rotate 45 degrees for diagonal placement
                RotateAngle = 45,

                // Place the stamp in the center of each page
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center,

                // Make the watermark semi‑transparent
                Opacity = 0.3,

                // Ensure the stamp is drawn over page content
                Background = false
            };

            // Add the stamp to every page (must call AddStamp on each Page)
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(stamp);
            }

            // Save the resulting PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPdf}'.");
    }
}