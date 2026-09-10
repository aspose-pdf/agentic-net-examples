using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string logoImage = "logo.png";           // company logo file
        const string outputPdf = "output.pdf";         // result PDF

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

        // Load the PDF document (lifecycle: create & load)
        using (Document doc = new Document(inputPdf))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page firstPage = doc.Pages[1];

            // Create an ImageStamp for the logo
            ImageStamp logoStamp = new ImageStamp(logoImage)
            {
                // Place the stamp behind the page content (false = foreground)
                Background = false,
                // Fully opaque
                Opacity = 1.0f,
                // Center the stamp horizontally and vertically
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center
            };

            // Add the stamp to the first page only
            firstPage.AddStamp(logoStamp);

            // Save the modified PDF (lifecycle: save)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Logo added to first page and saved as '{outputPdf}'.");
    }
}