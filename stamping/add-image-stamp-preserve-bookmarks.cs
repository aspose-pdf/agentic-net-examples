using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // for alignment enums

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";      // source PDF with bookmarks/outlines
        const string stampImage = "logo.png";       // image to use as stamp
        const string outputPdf  = "output.pdf";     // result PDF (bookmarks preserved)

        // Validate input files
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

        // Load the existing PDF (bookmarks and outline are loaded automatically)
        using (Document doc = new Document(inputPdf))
        {
            // Create an image stamp – the same instance can be reused for all pages
            ImageStamp imgStamp = new ImageStamp(stampImage)
            {
                // Example visual settings – adjust as needed
                Background          = false,                     // stamp on top of page content
                Opacity             = 0.7f,                      // semi‑transparent
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center
            };

            // Apply the stamp to every page; bookmarks/outlines remain untouched
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(imgStamp);
            }

            // Save the modified PDF; existing bookmarks and outline are preserved
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Image stamp added. Output saved to '{outputPdf}'.");
    }
}