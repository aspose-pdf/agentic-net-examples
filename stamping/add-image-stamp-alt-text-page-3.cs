using System;
using System.IO;
using Aspose.Pdf;                     // Core API (Document, Page, ImageStamp, etc.)

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputPdf = "output.pdf";         // result PDF
        const string stampImagePath = "logo.png";      // image to stamp
        const string altText = "Company logo for branding";

        // Ensure input files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(stampImagePath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImagePath}");
            return;
        }

        // Load the PDF (lifecycle: create -> load -> save)
        using (Document doc = new Document(inputPdf))
        {
            // Verify that the document has at least three pages
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("The document has fewer than 3 pages.");
                return;
            }

            // Create an ImageStamp from the image file
            ImageStamp imgStamp = new ImageStamp(stampImagePath);

            // Set alternative text for accessibility
            imgStamp.AlternativeText = altText;

            // Optional: position the stamp (example: top‑right corner)
            imgStamp.HorizontalAlignment = HorizontalAlignment.Right;
            imgStamp.VerticalAlignment   = VerticalAlignment.Top;
            // Use XIndent/YIndent instead of the non‑existent Margin property
            imgStamp.XIndent = 10f; // offset from the left edge (used together with alignment)
            imgStamp.YIndent = 10f; // offset from the bottom edge (used together with alignment)

            // Add the stamp to page 3 (pages are 1‑based)
            Page pageThree = doc.Pages[3];
            pageThree.AddStamp(imgStamp);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Image stamp with alt text added to page 3 and saved as '{outputPdf}'.");
    }
}
