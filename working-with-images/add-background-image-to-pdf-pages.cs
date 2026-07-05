using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";      // source PDF
        const string outputPdf = "output.pdf";     // result PDF with background image
        const string bgImage   = "background.png"; // background image file (any supported format)

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        if (!File.Exists(bgImage))
        {
            Console.Error.WriteLine($"Background image not found: {bgImage}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Iterate over all pages (1‑based indexing)
            foreach (Page page in doc.Pages)
            {
                // Create an image stamp from the background image file
                ImageStamp imgStamp = new ImageStamp(bgImage)
                {
                    // Place the stamp behind the page content
                    Background = true,
                    // Set opacity to 30 % (0.3)
                    Opacity = 0.3
                };

                // Apply the stamp to the current page
                page.AddStamp(imgStamp);
            }

            // Save the modified document
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Background image added to all pages. Saved as '{outputPdf}'.");
    }
}