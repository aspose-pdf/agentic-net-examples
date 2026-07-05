using System;
using System.IO;
using Aspose.Pdf;

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

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Create an ImageStamp with the watermark image
            ImageStamp imgStamp = new ImageStamp(imagePath)
            {
                // Rotate the stamp by 45 degrees for diagonal placement
                RotateAngle = 45,

                // Optional: make the stamp semi‑transparent
                Opacity = 0.5,

                // Position the stamp in the center of each page
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center,

                // Place the stamp over the page content (false = foreground)
                Background = false
            };

            // Apply the stamp to every page (page indexing is 1‑based)
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(imgStamp);
            }

            // Save the modified PDF (lifecycle rule: save inside using)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPdf}'.");
    }
}