using System;
using System.IO;
using Aspose.Pdf;               // Core API
using Aspose.Pdf.Facades;      // For PdfPageStamp (inherits from Stamp)

class Program
{
    static void Main()
    {
        const string targetPdfPath = "target.pdf";      // PDF to receive the background stamp
        const string stampPdfPath  = "stampSource.pdf"; // PDF containing the page to use as stamp
        const string outputPdfPath = "output.pdf";      // Resulting PDF

        // Verify input files exist
        if (!File.Exists(targetPdfPath))
        {
            Console.Error.WriteLine($"Target PDF not found: {targetPdfPath}");
            return;
        }
        if (!File.Exists(stampPdfPath))
        {
            Console.Error.WriteLine($"Stamp source PDF not found: {stampPdfPath}");
            return;
        }

        try
        {
            // Load the document that will receive the background stamp
            using (Document targetDoc = new Document(targetPdfPath))
            // Load the document that provides the stamp page (use first page as example)
            using (Document stampDoc = new Document(stampPdfPath))
            {
                // Choose which page from the stamp document to use (1‑based index)
                const int stampPageIndex = 1;
                Aspose.Pdf.Page stampPage = stampDoc.Pages[stampPageIndex];

                // Iterate over each page of the target document
                foreach (Aspose.Pdf.Page page in targetDoc.Pages)
                {
                    // Create a PdfPageStamp using the selected stamp page
                    PdfPageStamp pageStamp = new PdfPageStamp(stampPage);

                    // Set the stamp to be drawn as background (behind existing content)
                    pageStamp.Background = true;

                    // Optional: adjust opacity, scaling, alignment, etc.
                    // pageStamp.Opacity = 0.5;          // 50% transparent
                    // pageStamp.Zoom = 1.0;            // No scaling
                    // pageStamp.HorizontalAlignment = HorizontalAlignment.Center;
                    // pageStamp.VerticalAlignment   = VerticalAlignment.Center;

                    // Apply the stamp to the current page
                    page.AddStamp(pageStamp);
                }

                // Save the modified document
                targetDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"Background stamp applied successfully. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}