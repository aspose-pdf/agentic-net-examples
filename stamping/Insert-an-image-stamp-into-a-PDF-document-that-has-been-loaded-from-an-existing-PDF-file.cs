using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // source PDF
        const string imagePath = "stamp.png";   // image to use as stamp
        const string outputPdf = "output.pdf";  // result PDF

        // Validate input files
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Load the existing PDF (lifecycle: using ensures disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Create an ImageStamp from the image file
            ImageStamp imgStamp = new ImageStamp(imagePath);

            // Optional visual settings for the stamp
            imgStamp.HorizontalAlignment = HorizontalAlignment.Center; // center horizontally
            imgStamp.VerticalAlignment   = VerticalAlignment.Bottom; // place near bottom
            imgStamp.Opacity = 0.5; // semi‑transparent

            // Add the stamp to the first page (pages are 1‑based)
            doc.Pages[1].AddStamp(imgStamp);

            // Save the modified PDF (PDF format, no extra SaveOptions needed)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Image stamp added and saved to '{outputPdf}'.");
    }
}