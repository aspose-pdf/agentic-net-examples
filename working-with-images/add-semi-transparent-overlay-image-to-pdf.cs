using System;
using System.IO;
using Aspose.Pdf;                     // Core Aspose.Pdf namespace
using Aspose.Pdf.Annotations;        // For stamp-related classes (if needed)

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath   = "input.pdf";
        const string overlayImgPath = "overlay.png";   // Image to be used as semi‑transparent overlay
        const string outputPdfPath  = "output.pdf";

        // Verify files exist
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

        // Load the source PDF
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int i = 1; i <= pdfDoc.Pages.Count; i++)
            {
                Page page = pdfDoc.Pages[i];

                // Create an ImageStamp that uses the overlay image file
                // Fully qualify the type to avoid ambiguity with System.Drawing.Image
                Aspose.Pdf.ImageStamp imgStamp = new Aspose.Pdf.ImageStamp(overlayImgPath);

                // Make the stamp cover the whole page
                imgStamp.Width  = page.PageInfo.Width;
                imgStamp.Height = page.PageInfo.Height;
                imgStamp.XIndent = 0;
                imgStamp.YIndent = 0;

                // Set semi‑transparent opacity (0.0 = fully transparent, 1.0 = opaque)
                imgStamp.Opacity = 0.3f;   // 30 % opacity

                // Place the stamp on top of existing content (Background = false)
                imgStamp.Background = false;

                // Add the stamp to the current page
                page.AddStamp(imgStamp);
            }

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Overlay applied and saved to '{outputPdfPath}'.");
    }
}