using System;
using System.IO;
using Aspose.Pdf;               // Core API
using Aspose.Pdf.Facades;      // For ImageStamp (inherits from Stamp)

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";      // source PDF with embedded files
        const string outputPdf = "output.pdf";     // result PDF
        const string stampImagePath = "stamp.png"; // image to use as stamp

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdf}");
            return;
        }

        if (!File.Exists(stampImagePath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImagePath}");
            return;
        }

        // Load the original PDF (embedded files are kept automatically)
        using (Document doc = new Document(inputPdf))
        {
            // Create an ImageStamp – this stamp can be reused for all pages
            ImageStamp imgStamp = new ImageStamp(stampImagePath)
            {
                // Example positioning – bottom‑right corner with some margin
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment   = VerticalAlignment.Bottom,
                // Optional: set opacity, background flag, etc.
                Opacity = 0.5f,
                Background = false
            };

            // Apply the stamp to every page (Aspose.Pdf uses 1‑based page indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                page.AddStamp(imgStamp);   // Page.AddStamp adds the stamp to the page
            }

            // Save the modified PDF – embedded files remain intact
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Image stamp added and saved to '{outputPdf}'.");
    }
}