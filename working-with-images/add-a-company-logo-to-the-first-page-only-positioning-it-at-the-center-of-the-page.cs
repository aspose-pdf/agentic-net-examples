using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string logoPath  = "logo.png";           // company logo image
        const string outputPdf = "output.pdf";         // result PDF

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(logoPath))
        {
            Console.Error.WriteLine($"Logo image not found: {logoPath}");
            return;
        }

        // Load the PDF document (using the required create/load rule)
        using (Document doc = new Document(inputPdf))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page firstPage = doc.Pages[1];

            // Create an ImageStamp with the logo image
            ImageStamp logoStamp = new ImageStamp(logoPath)
            {
                // Center the stamp horizontally and vertically on the page
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center,

                // Optional: make the logo semi‑transparent
                Opacity = 1.0f
            };

            // Add the stamp to the first page only (per‑page AddStamp)
            firstPage.AddStamp(logoStamp);

            // Save the modified PDF (using the required save rule)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Logo added to first page and saved as '{outputPdf}'.");
    }
}