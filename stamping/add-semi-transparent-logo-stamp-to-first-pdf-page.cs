using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";      // source PDF
        const string logoImage = "logo.png";       // logo to stamp
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
            // Create an image stamp from the logo file (ImageStamp has a string ctor)
            ImageStamp logoStamp = new ImageStamp(logoImage);

            // Set semi‑transparent opacity (0.0 = fully transparent, 1.0 = opaque)
            logoStamp.Opacity = 0.5f;

            // Optionally position the stamp (e.g., top‑left corner)
            // You can adjust XIndent/YIndent, HorizontalAlignment, etc.
            logoStamp.HorizontalAlignment = HorizontalAlignment.Left;
            logoStamp.VerticalAlignment   = VerticalAlignment.Top;
            logoStamp.LeftMargin   = 10;   // distance from left edge
            logoStamp.TopMargin    = 10;   // distance from top edge

            // Add the stamp to the first page (pages are 1‑based)
            doc.Pages[1].AddStamp(logoStamp);

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Logo stamp applied. Output saved to '{outputPdf}'.");
    }
}