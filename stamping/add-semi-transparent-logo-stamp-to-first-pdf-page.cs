using System;
using System.IO;
using Aspose.Pdf;                     // Core API
using Aspose.Pdf.Facades;            // Not needed for ImageStamp, but kept if other facades are used

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";      // source PDF
        const string logoImage = "logo.png";       // logo to stamp
        const string outputPdf = "output.pdf";     // result PDF

        // Verify files exist
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

        // Load the PDF document (using statement ensures proper disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Create an ImageStamp from the logo file
            ImageStamp logoStamp = new ImageStamp(logoImage);

            // Set stamp properties – semi‑transparent and centered on the page
            logoStamp.Opacity = 0.5;                                 // 50 % opacity
            logoStamp.Background = false;                           // stamp on top of content
            logoStamp.HorizontalAlignment = HorizontalAlignment.Center;
            logoStamp.VerticalAlignment   = VerticalAlignment.Center;

            // Add the stamp to the first page (pages are 1‑based)
            Page firstPage = doc.Pages[1];
            firstPage.AddStamp(logoStamp);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Logo stamp added and saved to '{outputPdf}'.");
    }
}