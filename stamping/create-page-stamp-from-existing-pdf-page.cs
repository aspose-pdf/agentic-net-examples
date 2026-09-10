using System;
using System.IO;
using Aspose.Pdf;               // Core API (Document, Page, PdfPageStamp, etc.)

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string sourcePdfPath = "source.pdf";   // PDF containing the page to be used as a stamp template
        const string targetPdfPath = "target.pdf";   // PDF where the stamp will be applied
        const string outputPdfPath = "output.pdf";   // Resulting PDF

        // Verify input files exist
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdfPath}");
            return;
        }
        if (!File.Exists(targetPdfPath))
        {
            Console.Error.WriteLine($"Target file not found: {targetPdfPath}");
            return;
        }

        try
        {
            // Load the source document (the page that will become the stamp)
            using (Document sourceDoc = new Document(sourcePdfPath))
            {
                // Aspose.Pdf uses 1‑based page indexing
                Page stampTemplatePage = sourceDoc.Pages[1];

                // Load the target document (the document to receive the stamp)
                using (Document targetDoc = new Document(targetPdfPath))
                {
                    // Choose the page on which the stamp will be placed
                    Page targetPage = targetDoc.Pages[1];

                    // Create a PdfPageStamp from the template page
                    PdfPageStamp pageStamp = new PdfPageStamp(stampTemplatePage)
                    {
                        // Example property customizations
                        Background = false,                                 // Stamp appears on top
                        Opacity = 0.7f,                                     // Semi‑transparent
                        HorizontalAlignment = Aspose.Pdf.HorizontalAlignment.Center,
                        VerticalAlignment   = Aspose.Pdf.VerticalAlignment.Center,
                        // You can also set margins, zoom, rotation, etc., as needed
                    };

                    // Apply the stamp to the target page
                    targetPage.AddStamp(pageStamp);

                    // Save the modified document
                    targetDoc.Save(outputPdfPath);
                }
            }

            Console.WriteLine($"Stamp applied successfully. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}