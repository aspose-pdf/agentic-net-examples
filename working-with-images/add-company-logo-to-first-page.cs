using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";      // source PDF
        const string logoImage = "logo.png";       // company logo image
        const string outputPdf = "output.pdf";     // result PDF

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

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document doc = new Document(inputPdf))
        {
            // Create an image stamp for the logo
            ImageStamp logoStamp = new ImageStamp(logoImage)
            {
                // Center the stamp on the page
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center,
                // Optional visual settings
                Background = false,
                Opacity    = 1.0f
            };

            // Add the stamp only to the first page (pages are 1‑based)
            Page firstPage = doc.Pages[1];
            firstPage.AddStamp(logoStamp);

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Logo added to first page. Saved as '{outputPdf}'.");
    }
}