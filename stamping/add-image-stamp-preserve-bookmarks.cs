using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // not needed but harmless
using Aspose.Pdf.Facades;   // not needed for stamping, but included for completeness

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string stampPath  = "logo.png";   // image to use as stamp
        const string outputPath = "output_stamped.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        if (!File.Exists(stampPath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampPath}");
            return;
        }

        // Load the PDF document (using statement ensures proper disposal)
        using (Document pdfDoc = new Document(inputPath))
        {
            // Create an ImageStamp from the image file
            ImageStamp imgStamp = new ImageStamp(stampPath)
            {
                // Position the stamp in the centre of each page
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center,

                // Optional visual settings
                Opacity   = 0.5,   // semi‑transparent
                Background = false // stamp appears on top of page content
            };

            // Apply the stamp to every page. Page collection uses 1‑based indexing.
            for (int i = 1; i <= pdfDoc.Pages.Count; i++)
            {
                Page page = pdfDoc.Pages[i];
                page.AddStamp(imgStamp);
            }

            // Save the modified document. This preserves existing bookmarks and outline.
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}