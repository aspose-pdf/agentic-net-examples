using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;   // ImageStamp resides here

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";      // source PDF
        const string logoImage = "logo.png";       // company logo file
        const string outputPdf = "output_with_header.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(logoImage))
        {
            Console.Error.WriteLine($"Logo image not found: {logoImage}");
            return;
        }

        // Load the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Iterate over all pages (1‑based indexing)
            foreach (Page page in doc.Pages)
            {
                // Create an image stamp from the logo file
                ImageStamp logoStamp = new ImageStamp(logoImage)
                {
                    // Position the logo at the top‑center of the page
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment   = VerticalAlignment.Top,
                    // Optional: set margins or explicit offsets
                    TopMargin    = 20,   // distance from the top edge
                    // Ensure the stamp is drawn over the page content
                    Background   = false,
                    Opacity      = 1.0f
                };

                // Add the stamp to the current page
                page.AddStamp(logoStamp);
            }

            // Save the modified document (PDF format)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Header with logo added to all pages. Saved as '{outputPdf}'.");
    }
}