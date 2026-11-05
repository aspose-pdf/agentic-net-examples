using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPdfPath = "output.pdf";
        const string highResImagePath = "highres.jpg";

        if (!File.Exists(highResImagePath))
        {
            Console.Error.WriteLine($"Image file not found: {highResImagePath}");
            return;
        }

        // Load the image to obtain its pixel dimensions. Fully qualify System.Drawing.Image to avoid ambiguity.
        using (System.Drawing.Image sysImg = System.Drawing.Image.FromFile(highResImagePath))
        {
            const float sourceDpi = 96f; // typical screen DPI; adjust if needed
            double widthPt = sysImg.Width * 72.0 / sourceDpi;
            double heightPt = sysImg.Height * 72.0 / sourceDpi;

            // Create a new PDF document.
            Document pdfDoc = new Document();
            // Add a blank page.
            Page page = pdfDoc.Pages.Add();

            // Create an ImageStamp for absolute positioning.
            ImageStamp stamp = new ImageStamp(highResImagePath)
            {
                Width = widthPt,   // preserve original size (in points)
                Height = heightPt,
                XIndent = 100,     // distance from the left edge of the page
                YIndent = 500      // distance from the bottom edge of the page
            };

            // Place the stamp on the page.
            page.AddStamp(stamp);

            // Save the PDF.
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with high‑resolution image saved to '{outputPdfPath}'.");
    }
}
