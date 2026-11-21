using System;
using System.IO;
using Aspose.Pdf;               // Core PDF API (Document, Page, ImageStamp, etc.)

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";      // Original PDF with embedded files
        const string stampImg  = "stamp.png";      // Image to use as stamp
        const string outputPdf = "output_stamped.pdf";

        // Verify required files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(stampImg))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImg}");
            return;
        }

        // Load the PDF (embedded files are loaded automatically)
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Configure the image stamp
            ImageStamp imgStamp = new ImageStamp(stampImg)
            {
                // Position and size can be adjusted as needed
                // Here we place the stamp at the bottom‑right corner of each page
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment   = VerticalAlignment.Bottom,
                // Keep the original image dimensions; set Opacity if a translucent effect is desired
                Opacity = 0.8,
                // Ensure the stamp is drawn over page content (false = background)
                Background = false
            };

            // Apply the stamp to every page individually (PageCollection has no AddStamp method)
            foreach (Page page in pdfDoc.Pages)
            {
                page.AddStamp(imgStamp);
            }

            // Save the modified PDF; embedded files are preserved automatically
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"Image stamp added and saved to '{outputPdf}'. Embedded files remain intact.");
    }
}