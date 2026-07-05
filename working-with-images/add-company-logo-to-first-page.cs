using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing; // ImageStamp, HorizontalAlignment, VerticalAlignment

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";      // source PDF
        const string logoImagePath = "logo.png";       // company logo image
        const string outputPdfPath = "output.pdf";     // result PDF

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

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPdfPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page firstPage = doc.Pages[1];

            // Create an ImageStamp for the logo using the Drawing namespace.
            ImageStamp logoStamp = new ImageStamp(logoImagePath)
            {
                // Position the stamp at the center of the page.
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center,

                // Opacity (0.0 = fully transparent, 1.0 = fully opaque)
                Opacity = 1.0f
            };

            // Add the stamp to the first page only.
            firstPage.AddStamp(logoStamp);

            // Save the modified PDF.
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Logo added to first page. Saved as '{outputPdfPath}'.");
    }
}
