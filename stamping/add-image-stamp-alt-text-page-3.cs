using System;
using System.IO;
using Aspose.Pdf;               // Core API (Document, Page, ImageStamp, etc.)

class Program
{
    static void Main()
    {
        // Input PDF path (must exist) and output PDF path.
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_with_stamp.pdf";
        const string stampImagePath = "logo.png";   // Image to be used as stamp
        const string altText = "Company logo for accessibility";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        if (!File.Exists(stampImagePath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImagePath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPdf))
        {
            // Ensure the document has at least three pages.
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("The document does not contain a third page.");
                return;
            }

            // Create an ImageStamp from the image file.
            ImageStamp imgStamp = new ImageStamp(stampImagePath);

            // Set the alternative text for accessibility.
            imgStamp.AlternativeText = altText;

            // Optional: position the stamp (example: bottom‑right corner).
            // XIndent/YIndent are measured from the left/bottom of the page.
            imgStamp.XIndent = 400;   // horizontal offset
            imgStamp.YIndent = 50;    // vertical offset
            imgStamp.HorizontalAlignment = HorizontalAlignment.Right;
            imgStamp.VerticalAlignment   = VerticalAlignment.Bottom;

            // Retrieve page three (Aspose.Pdf uses 1‑based indexing).
            Page pageThree = doc.Pages[3];

            // Add the stamp to the page.
            pageThree.AddStamp(imgStamp);

            // Save the modified document.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Image stamp with alt text added to page 3 and saved as '{outputPdf}'.");
    }
}