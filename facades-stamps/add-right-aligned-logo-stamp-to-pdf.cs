using System;
using System.IO;
using Aspose.Pdf;                     // Core API (Document, Page, ImageStamp, HorizontalAlignment, etc.)
using Aspose.Pdf.Facades;            // Facades API (optional, not used here)

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string logoPath  = "logo.png";
        const string outputPdf = "output_with_logo.pdf";

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

        // Load the source PDF inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPdf))
        {
            // Create an ImageStamp from the logo file.
            ImageStamp logoStamp = new ImageStamp(logoPath);

            // Align the stamp to the right margin of the page.
            logoStamp.HorizontalAlignment = HorizontalAlignment.Right;

            // Optional: set vertical alignment, margins, opacity, etc.
            logoStamp.VerticalAlignment   = VerticalAlignment.Center;
            logoStamp.Opacity             = 0.8f;               // 80% opacity
            logoStamp.RightMargin         = 10;                 // 10 points from the right edge
            logoStamp.TopMargin           = 0;                  // no top margin (centered vertically)

            // Apply the stamp to every page in the document.
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(logoStamp);
            }

            // Save the modified PDF.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Logo stamp added and saved to '{outputPdf}'.");
    }
}
