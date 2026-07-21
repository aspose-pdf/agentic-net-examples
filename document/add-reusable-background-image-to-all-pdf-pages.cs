using System;
using System.IO;
using Aspose.Pdf;               // Core API (Document, Page, PdfPageStamp)

class Program
{
    static void Main()
    {
        const string inputPdf    = "input.pdf";      // PDF to which the background will be added
        const string templatePdf = "template.pdf";   // PDF containing the reusable background (single page)
        const string outputPdf   = "output.pdf";    // Resulting PDF

        if (!File.Exists(inputPdf) || !File.Exists(templatePdf))
        {
            Console.Error.WriteLine("Input or template file not found.");
            return;
        }

        // Load the target document and the template (background) document.
        using (Document targetDoc   = new Document(inputPdf))
        using (Document templateDoc = new Document(templatePdf))
        {
            // Create a stamp from the first page of the template.
            // PdfPageStamp can be reused for all pages.
            PdfPageStamp backgroundStamp = new PdfPageStamp(templateDoc.Pages[1]);

            // Place the stamp behind existing page content.
            backgroundStamp.Background = true;

            // Optional: adjust opacity, scaling, alignment, etc.
            // backgroundStamp.Opacity = 0.8;
            // backgroundStamp.HorizontalAlignment = HorizontalAlignment.Center;
            // backgroundStamp.VerticalAlignment   = VerticalAlignment.Center;

            // Apply the stamp to every page of the target document.
            foreach (Page page in targetDoc.Pages)
            {
                page.AddStamp(backgroundStamp);
            }

            // Save the modified document.
            targetDoc.Save(outputPdf);
        }

        Console.WriteLine($"Background image added to all pages. Saved as '{outputPdf}'.");
    }
}