using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string overlayPng = "overlay.png";   // transparent PNG
        const string outputPdf = "output.pdf";

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

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Create an ImageStamp for the PNG overlay
            ImageStamp pngStamp = new ImageStamp(overlayPng)
            {
                // Ensure the stamp is placed on top (Background = false is default)
                Background = false,
                // Adjust opacity if needed (0.0 = fully transparent, 1.0 = opaque)
                Opacity = 0.5f,
                // Center the overlay on each page
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center
            };

            // Add the stamp to every page
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(pngStamp);
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Overlay applied and saved to '{outputPdf}'.");
    }
}