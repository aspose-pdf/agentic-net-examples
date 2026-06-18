using System;
using System.IO;
using Aspose.Pdf;               // Core API for Document, Page, ImageStamp
using Aspose.Pdf.Facades;      // For ImageStamp (if needed, otherwise core namespace suffices)

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // source PDF
        const string outputPdf = "output.pdf";  // result PDF
        const string logoPath  = "logo.png";    // company logo image

        // Verify files exist
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

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Create an ImageStamp for the logo
            ImageStamp logoStamp = new ImageStamp(logoPath)
            {
                // Center the stamp horizontally and vertically on the page
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center,

                // Optional visual settings
                Background = false,   // place over page content
                Opacity    = 1.0f     // fully opaque
            };

            // Add the stamp only to the first page (pages are 1‑based)
            pdfDoc.Pages[1].AddStamp(logoStamp);

            // Save the modified PDF (lifecycle rule: save inside using block)
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"Logo added to first page and saved as '{outputPdf}'.");
    }
}