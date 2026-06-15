using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // needed for ImageStamp (inherits from Stamp)

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "watermarked.pdf";
        const string imagePath = "logo.png"; // watermark image file

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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Create an ImageStamp once; it can be reused for all pages
            ImageStamp stamp = new ImageStamp(imagePath)
            {
                // 20% opacity for subtle branding
                Opacity = 0.2,
                // Position the stamp (optional). Here we center it on the page.
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center,
                // Keep the stamp on top of page content
                Background = false
            };

            // Apply the stamp to each page individually
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