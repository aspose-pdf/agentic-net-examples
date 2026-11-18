using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string backgroundImagePath = "background.png";

        // Verify files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(backgroundImagePath))
        {
            Console.Error.WriteLine($"Background image not found: {backgroundImagePath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create an ImageStamp that will be used as a background image
            ImageStamp bgStamp = new ImageStamp(backgroundImagePath)
            {
                // Place the image on the page background
                Background = true,
                // Set opacity to 30% (0.3)
                Opacity = 0.3
                // No need to set rectangle; default uses full page size
            };

            // Iterate over all pages (1‑based indexing is handled by the foreach)
            foreach (Page page in pdfDoc.Pages)
            {
                // Add the background stamp to each page
                page.AddStamp(bgStamp);
            }

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Background image applied and saved to '{outputPdfPath}'.");
    }
}