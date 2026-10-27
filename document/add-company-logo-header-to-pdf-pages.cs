using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string logoImagePath = "logo.png";
        const string outputPdfPath = "output_with_header.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(logoImagePath))
        {
            Console.Error.WriteLine($"Logo image not found: {logoImagePath}");
            return;
        }

        // Load the existing PDF inside a using block for deterministic disposal.
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Iterate over all pages (1‑based indexing).
            for (int i = 1; i <= pdfDoc.Pages.Count; i++)
            {
                Page page = pdfDoc.Pages[i];

                // Create an ImageStamp for the logo.
                ImageStamp logoStamp = new ImageStamp(logoImagePath)
                {
                    // Position the stamp at the top of the page.
                    // Origin (0,0) is bottom‑left, so TopMargin = page height - desired height.
                    TopMargin    = page.PageInfo.Height - 50, // 50 points height for the header
                    LeftMargin   = 0,
                    Width        = page.PageInfo.Width,
                    Height       = 50,
                    // Align the image within the stamp rectangle.
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment   = VerticalAlignment.Top,
                    // Ensure the stamp is drawn over the page content.
                    Background = false
                    // BackgroundColor property does not exist on ImageStamp; omitted.
                };

                // Add the stamp to the current page.
                page.AddStamp(logoStamp);
            }

            // Save the modified PDF.
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Header with logo added to all pages. Saved as '{outputPdfPath}'.");
    }
}