using System;
using System.IO;
using Aspose.Pdf;               // Core API
using Aspose.Pdf.Facades;      // For PdfPageStamp (inherits from Stamp)

class Program
{
    static void Main()
    {
        const string inputPdf    = "input.pdf";            // PDF to be processed
        const string templatePdf = "background_template.pdf"; // PDF containing the background page
        const string outputPdf   = "output.pdf";

        // Verify that source files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(templatePdf))
        {
            Console.Error.WriteLine($"Template PDF not found: {templatePdf}");
            return;
        }

        // Load both documents inside using blocks for deterministic disposal
        using (Document doc = new Document(inputPdf))
        using (Document template = new Document(templatePdf))
        {
            // Create a stamp from the first page of the template.
            // PdfPageStamp implements the Stamp base class and can be applied to any page.
            PdfPageStamp stamp = new PdfPageStamp(template.Pages[1]);

            // Place the stamp behind existing page content.
            stamp.Background = true;

            // Optionally adjust opacity, scaling, alignment, etc.
            // stamp.Opacity = 0.8f;
            // stamp.HorizontalAlignment = HorizontalAlignment.Center;
            // stamp.VerticalAlignment   = VerticalAlignment.Center;

            // Apply the stamp to every page of the target document.
            foreach (Page page in doc.Pages)   // Pages are 1‑based; foreach abstracts the indexing.
            {
                page.AddStamp(stamp);
            }

            // Save the modified PDF. No SaveOptions needed for PDF output.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Background image applied to all pages. Saved as '{outputPdf}'.");
    }
}