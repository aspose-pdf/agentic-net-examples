using System;
using System.IO;
using Aspose.Pdf;               // Core API (Document, Page, ImageStamp, etc.)

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string overlayPng = "overlay.png";       // transparent PNG to overlay
        const string outputPdf = "output.pdf";         // result PDF

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(overlayPng))
        {
            Console.Error.WriteLine($"Overlay image not found: {overlayPng}");
            return;
        }

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document doc = new Document(inputPdf))
        {
            // Iterate over each page and add the PNG as a stamp.
            foreach (Page page in doc.Pages)
            {
                // Create an ImageStamp from the PNG file.
                ImageStamp stamp = new ImageStamp(overlayPng)
                {
                    // Ensure the stamp is placed above existing content.
                    // Background = false (default) means top layer.
                    Background = false,

                    // Optional: control opacity (0.0 = fully transparent, 1.0 = opaque).
                    Opacity = 0.8f,

                    // Position the stamp – here we center it on the page.
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment   = VerticalAlignment.Center
                };

                // Add the stamp to the current page (rule: call AddStamp per page).
                page.AddStamp(stamp);
            }

            // Save the modified PDF (lifecycle rule: use Document.Save).
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Overlay applied and saved to '{outputPdf}'.");
    }
}