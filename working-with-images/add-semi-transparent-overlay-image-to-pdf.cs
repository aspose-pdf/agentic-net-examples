using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string overlayImgPath = "overlay.png";
        const string outputPdfPath = "output.pdf";

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

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Iterate over all pages (1‑based indexing).
            for (int i = 1; i <= pdfDoc.Pages.Count; i++)
            {
                Page page = pdfDoc.Pages[i];

                // Create an ImageStamp for the overlay image.
                ImageStamp overlayStamp = new ImageStamp(overlayImgPath)
                {
                    // Place the stamp on top of existing content.
                    Background = false,

                    // Resize the stamp to cover the entire page.
                    Width = page.Rect.Width,
                    Height = page.Rect.Height,

                    // Position at the lower‑left corner of the page.
                    XIndent = 0,
                    YIndent = 0
                };

                // Apply the stamp to the current page.
                page.AddStamp(overlayStamp);
            }

            // Save the modified PDF.
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Overlay applied and saved to '{outputPdfPath}'.");
    }
}
