using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";   // source PDF
        const string outputPdfPath = "output.pdf";  // result PDF
        const string backgroundImg = "background.png"; // image to use as background

        // Validate input files
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(backgroundImg))
        {
            Console.Error.WriteLine($"Background image not found: {backgroundImg}");
            return;
        }

        // Load the PDF document (load rule)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Iterate over all pages (pages are 1‑based internally)
            foreach (Page page in pdfDoc.Pages)
            {
                // Create an image stamp (create rule)
                ImageStamp imgStamp = new ImageStamp(backgroundImg);

                // Place the stamp behind page content
                imgStamp.Background = true;

                // Set opacity to 30 % (0.3)
                imgStamp.Opacity = 0.3;

                // Stretch the stamp to cover the whole page
                imgStamp.Width  = page.Rect.Width;
                imgStamp.Height = page.Rect.Height;
                imgStamp.HorizontalAlignment = HorizontalAlignment.Center;
                imgStamp.VerticalAlignment   = VerticalAlignment.Center;

                // Add the stamp to the current page (per‑page AddStamp)
                page.AddStamp(imgStamp);
            }

            // Save the modified PDF (save rule)
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Background image applied and saved to '{outputPdfPath}'.");
    }
}