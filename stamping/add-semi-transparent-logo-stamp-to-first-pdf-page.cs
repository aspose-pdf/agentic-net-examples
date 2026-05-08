using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string logoImage = "logo.png";
        const string outputPdf = "output.pdf";

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

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Create an image stamp from the logo file
            ImageStamp logoStamp = new ImageStamp(logoImage);

            // Make the stamp semi‑transparent (0.0 = fully transparent, 1.0 = opaque)
            logoStamp.Opacity = 0.5f;

            // Position the stamp – top‑right corner with a small margin
            logoStamp.HorizontalAlignment = HorizontalAlignment.Right;
            logoStamp.VerticalAlignment   = VerticalAlignment.Top;
            logoStamp.RightMargin  = 10; // margin from the right edge
            logoStamp.TopMargin    = 10; // margin from the top edge

            // Add the stamp to the first page (pages are 1‑based)
            doc.Pages[1].AddStamp(logoStamp);

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Semi‑transparent logo stamp added to first page and saved as '{outputPdf}'.");
    }
}