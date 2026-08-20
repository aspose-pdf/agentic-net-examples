using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string logoPath  = "logo.png";

        // Verify required files exist
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

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Create an image stamp from the logo file
            ImageStamp logoStamp = new ImageStamp(logoPath);

            // Set semi‑transparent opacity (0.0 = fully transparent, 1.0 = opaque)
            logoStamp.Opacity = 0.5f;

            // Position the stamp – top‑left corner with some margins
            logoStamp.HorizontalAlignment = HorizontalAlignment.Left;
            logoStamp.VerticalAlignment   = VerticalAlignment.Top;
            logoStamp.LeftMargin   = 20; // points from the left edge
            logoStamp.TopMargin    = 20; // points from the top edge

            // Add the stamp to the first page (Page.AddStamp expects a Stamp)
            doc.Pages[1].AddStamp(logoStamp);

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Semi‑transparent logo stamp added. Output saved to '{outputPdf}'.");
    }
}