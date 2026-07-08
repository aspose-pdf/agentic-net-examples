using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the target PDF, the PDF page to use as background, and the result.
        const string targetPdfPath    = "target.pdf";
        const string backgroundPdfPath = "background.pdf";
        const string outputPdfPath    = "output.pdf";

        // Ensure input files exist.
        if (!File.Exists(targetPdfPath))
        {
            Console.Error.WriteLine($"Target PDF not found: {targetPdfPath}");
            return;
        }
        if (!File.Exists(backgroundPdfPath))
        {
            Console.Error.WriteLine($"Background PDF not found: {backgroundPdfPath}");
            return;
        }

        // Load both documents inside using blocks for deterministic disposal.
        using (Document targetDoc = new Document(targetPdfPath))
        using (Document backgroundDoc = new Document(backgroundPdfPath))
        {
            // Choose the page from the background PDF that will be used as the stamp.
            // Aspose.Pdf uses 1‑based indexing for pages.
            Page stampSourcePage = backgroundDoc.Pages[1];

            // Iterate over each page of the target document and apply the stamp.
            for (int i = 1; i <= targetDoc.Pages.Count; i++)
            {
                Page targetPage = targetDoc.Pages[i];

                // Create a new PdfPageStamp using the source page.
                PdfPageStamp pageStamp = new PdfPageStamp(stampSourcePage)
                {
                    // Place the stamp behind the page content.
                    Background = true,

                    // Optional: adjust opacity, scaling, or alignment as needed.
                    Opacity = 0.5f,               // 50% transparent
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment   = VerticalAlignment.Center
                };

                // Apply the stamp to the current page.
                targetPage.AddStamp(pageStamp);
            }

            // Save the modified document.
            targetDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with background stamp: {outputPdfPath}");
    }
}