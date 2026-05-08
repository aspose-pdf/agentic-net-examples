using System;
using System.IO;
using Aspose.Pdf;               // Core PDF API
using Aspose.Pdf.Annotations;   // For potential annotation handling (not needed here)

class Program
{
    static void Main()
    {
        // Input PDF with existing JavaScript actions
        const string inputPdf  = "input.pdf";
        // Image to be used as a stamp
        const string stampImage = "stamp.png";
        // Output PDF – JavaScript actions will be preserved
        const string outputPdf = "output.pdf";

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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Iterate over all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create an ImageStamp from the image file
                ImageStamp imgStamp = new ImageStamp(stampImage);

                // Example positioning – bottom‑right corner with a margin
                imgStamp.HorizontalAlignment = HorizontalAlignment.Right;
                imgStamp.VerticalAlignment   = VerticalAlignment.Bottom;
                imgStamp.RightMargin  = 20;   // 20 points from the right edge
                imgStamp.BottomMargin = 20;   // 20 points from the bottom edge

                // Optional: make the stamp semi‑transparent
                imgStamp.Opacity = 0.6f;

                // Add the stamp to the current page.
                // This operation does NOT remove existing JavaScript actions.
                page.AddStamp(imgStamp);
            }

            // Save the modified PDF. No SaveOptions are needed because we keep PDF format.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Image stamp added. Output saved to '{outputPdf}'.");
    }
}