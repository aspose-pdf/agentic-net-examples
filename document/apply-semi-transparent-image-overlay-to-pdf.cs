using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "watermarked.pdf";
        const string overlayImgPath = "overlay.png";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(overlayImgPath))
        {
            Console.Error.WriteLine($"Overlay image not found: {overlayImgPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Apply a semi‑transparent image stamp to each page
            foreach (Page page in pdfDoc.Pages)
            {
                // Create an image stamp from the overlay image
                ImageStamp imgStamp = new ImageStamp(overlayImgPath);

                // Set the stamp to be semi‑transparent (0.0 = fully transparent, 1.0 = opaque)
                imgStamp.Opacity = 0.3; // adjust as needed for desired transparency

                // Place the stamp on top of existing content
                imgStamp.Background = false;

                // Center the stamp on the page (optional positioning)
                imgStamp.HorizontalAlignment = HorizontalAlignment.Center;
                imgStamp.VerticalAlignment   = VerticalAlignment.Center;

                // Add the stamp to the current page
                page.AddStamp(imgStamp);
            }

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPdfPath}'.");
    }
}