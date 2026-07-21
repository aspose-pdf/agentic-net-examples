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

        // Load the PDF document (using rule: document-disposal-with-using)
        using (Document doc = new Document(inputPdf))
        {
            // Create an image stamp from the logo file
            ImageStamp stamp = new ImageStamp(logoImage);

            // Set semi‑transparent opacity (0.0 = fully transparent, 1.0 = opaque)
            stamp.Opacity = 0.5;

            // Position the stamp (optional – adjust as needed)
            stamp.HorizontalAlignment = HorizontalAlignment.Left;
            stamp.VerticalAlignment   = VerticalAlignment.Top;
            stamp.LeftMargin = 20;   // distance from left edge
            stamp.TopMargin  = 20;   // distance from top edge

            // Add the stamp to the first page (pages are 1‑based)
            doc.Pages[1].AddStamp(stamp);

            // Save the modified PDF (PDF format, no SaveOptions needed)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPdf}'.");
    }
}