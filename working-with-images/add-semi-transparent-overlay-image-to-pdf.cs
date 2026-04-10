using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the source PDF, overlay image, and the output PDF
        const string inputPdfPath   = "input.pdf";
        const string overlayImgPath = "overlay.png";
        const string outputPdfPath  = "output.pdf";

        // Verify that the required files exist
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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create an ImageStamp from the overlay image
            ImageStamp overlayStamp = new ImageStamp(overlayImgPath)
            {
                // Set semi‑transparent opacity (0.0 = fully transparent, 1.0 = opaque)
                Opacity = 0.5,
                // Place the stamp on top of existing content
                Background = false
            };

            // Apply the stamp to every page, scaling it to cover the entire page
            foreach (Page page in pdfDoc.Pages)
            {
                // Adjust stamp size to match the current page dimensions
                overlayStamp.Width  = page.PageInfo.Width;
                overlayStamp.Height = page.PageInfo.Height;
                overlayStamp.XIndent = 0;
                overlayStamp.YIndent = 0;

                // Add the stamp to the page
                page.AddStamp(overlayStamp);
            }

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Overlay applied and saved to '{outputPdfPath}'.");
    }
}