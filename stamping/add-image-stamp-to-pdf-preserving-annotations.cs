using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";      // source PDF with existing annotations
        const string outputPdf  = "output.pdf";     // result PDF
        const string stampImage = "stamp.png";      // image to use as stamp

        // Verify files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(stampImage))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImage}");
            return;
        }

        // Load the PDF (document disposal handled by using)
        using (Document doc = new Document(inputPdf))
        {
            // Select the page to stamp – Aspose.Pdf uses 1‑based indexing
            Page page = doc.Pages[1];

            // Create an image stamp
            ImageStamp imgStamp = new ImageStamp(stampImage);

            // Optional: configure appearance
            imgStamp.HorizontalAlignment = HorizontalAlignment.Center;
            imgStamp.VerticalAlignment   = VerticalAlignment.Center;
            imgStamp.Opacity = 0.5f; // semi‑transparent

            // Add the stamp to the page; existing annotations are preserved
            page.AddStamp(imgStamp);

            // Save the modified document (PDF format)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Image stamp added and saved to '{outputPdf}'.");
    }
}