using System;
using System.IO;
using Aspose.Pdf; // Core API – includes ImageStamp, HorizontalAlignment, etc.

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";      // Source PDF with embedded files
        const string outputPdf  = "output.pdf";     // Resulting PDF
        const string stampImage = "logo.png";       // Image to use as stamp

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        if (!File.Exists(stampImage))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImage}");
            return;
        }

        // Load the original PDF. Embedded files remain attached to the document.
        using (Document doc = new Document(inputPdf))
        {
            // Create an image stamp using the core Aspose.Pdf API.
            ImageStamp imgStamp = new ImageStamp(stampImage);
            imgStamp.Background          = false;               // Stamp on top of page content
            imgStamp.Opacity             = 0.5f;                // Semi‑transparent
            imgStamp.HorizontalAlignment = HorizontalAlignment.Right;
            imgStamp.VerticalAlignment   = VerticalAlignment.Bottom;
            // Optional: set explicit size or margins
            // imgStamp.Width  = 100;
            // imgStamp.Height = 50;
            // imgStamp.RightMargin = 10;
            // imgStamp.BottomMargin = 10;

            // Apply the stamp to every page individually.
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(imgStamp);
            }

            // Save the modified PDF. All original embedded files are preserved.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Image stamp added and saved to '{outputPdf}'.");
    }
}
