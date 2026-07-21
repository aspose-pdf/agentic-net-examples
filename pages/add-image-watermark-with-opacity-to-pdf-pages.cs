using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";      // source PDF
        const string watermark  = "logo.png";       // watermark image file
        const string outputPdf  = "watermarked.pdf"; // result PDF

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        if (!File.Exists(watermark))
        {
            Console.Error.WriteLine($"Watermark image not found: {watermark}");
            return;
        }

        // Load the PDF document (lifecycle: load)
        using (Document doc = new Document(inputPdf))
        {
            // Iterate over all pages and apply the image stamp
            foreach (Page page in doc.Pages)
            {
                // Create an ImageStamp with the watermark image
                ImageStamp imgStamp = new ImageStamp(watermark)
                {
                    // Set opacity to 20% (0.2)
                    Opacity = 0.2,
                    // Place the stamp behind the page content (optional)
                    Background = false,
                    // Center the stamp on the page (optional)
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment   = VerticalAlignment.Center
                };

                // Add the stamp to the current page (lifecycle: modify)
                page.AddStamp(imgStamp);
            }

            // Save the modified PDF (lifecycle: save)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPdf}'.");
    }
}