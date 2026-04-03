using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string overlayPngPath = "overlay.png";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(overlayPngPath))
        {
            Console.Error.WriteLine($"Overlay PNG not found: {overlayPngPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdfPath))
        {
            // Iterate over each page and add the PNG as a stamp
            foreach (Page page in doc.Pages)
            {
                // Create an ImageStamp from the PNG file
                ImageStamp stamp = new ImageStamp(overlayPngPath)
                {
                    // Ensure the stamp is placed on top of existing content
                    Background = false,          // false = top layer (default, but set explicitly)
                    // Optional: make the stamp cover the whole page
                    Width  = page.Rect.Width,
                    Height = page.Rect.Height,
                    // Optional: position at the lower‑left corner (origin)
                    XIndent = 0,
                    YIndent = 0,
                    // Preserve the PNG's own transparency; Opacity can be adjusted if needed
                    Opacity = 1.0f
                };

                // Add the stamp to the current page
                page.AddStamp(stamp);
            }

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Overlay applied and saved to '{outputPdfPath}'.");
    }
}