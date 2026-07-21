using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_with_header.pdf";
        const string logoPath  = "company_logo.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        if (!File.Exists(logoPath))
        {
            Console.Error.WriteLine($"Logo image not found: {logoPath}");
            return;
        }

        // Load the existing PDF document inside a using block for proper disposal.
        using (Document doc = new Document(inputPdf))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing).
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create an ImageStamp for the logo.
                ImageStamp logoStamp = new ImageStamp(logoPath)
                {
                    // Place the stamp at the top center of the page.
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment   = VerticalAlignment.Top,

                    // Optional visual tweaks.
                    Background = false,          // Draw over page content.
                    Opacity    = 0.9f,           // Slightly transparent.
                    TopMargin  = 10,             // Distance from the top edge.
                };

                // Add the stamp to the current page.
                page.AddStamp(logoStamp);
            }

            // Save the modified PDF.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Header with logo added to all pages. Saved as '{outputPdf}'.");
    }
}