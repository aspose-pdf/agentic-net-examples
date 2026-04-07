using System;
using System.IO;
using Aspose.Pdf;               // Core API
using Aspose.Pdf.Facades;      // For PdfPageStamp (inherits from Stamp)

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string templatePdfPath = "template.pdf";   // PDF containing the page to use as a stamp
        const string sourcePdfPath   = "source.pdf";     // PDF to which the stamp will be applied
        const string outputPdfPath   = "output.pdf";

        // Verify files exist
        if (!File.Exists(templatePdfPath))
        {
            Console.Error.WriteLine($"Template file not found: {templatePdfPath}");
            return;
        }
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdfPath}");
            return;
        }

        // Load the template PDF and obtain the page that will serve as the stamp.
        // Using statement ensures deterministic disposal (document-disposal-with-using rule).
        using (Document templateDoc = new Document(templatePdfPath))
        {
            // Aspose.Pdf uses 1‑based page indexing (page-indexing-one-based rule).
            Page stampSourcePage = templateDoc.Pages[1];

            // Create a PdfPageStamp from the selected page (PdfPageStamp constructor rule).
            PdfPageStamp pageStamp = new PdfPageStamp(stampSourcePage)
            {
                // Example property settings – optional.
                Background = false,          // Stamp appears on top of page content.
                Opacity    = 0.8,            // Slightly transparent.
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center,
                // You can also set Width/Height, Zoom, etc., as needed.
            };

            // Load the document that will receive the stamp.
            using (Document targetDoc = new Document(sourcePdfPath))
            {
                // Apply the stamp to each page of the target document.
                foreach (Page targetPage in targetDoc.Pages)
                {
                    // Page.AddStamp is the correct method (add-stamp-to-all-pages-call-per-page-not-on-collection rule).
                    targetPage.AddStamp(pageStamp);
                }

                // Save the modified document (save rule – Document.Save).
                targetDoc.Save(outputPdfPath);
                Console.WriteLine($"Stamped PDF saved to '{outputPdfPath}'.");
            }
        }
    }
}