using System;
using System.IO;
using Aspose.Pdf;               // Core PDF classes
using Aspose.Pdf.Facades;      // Not needed here but kept for completeness

class Program
{
    static void Main()
    {
        const string sourcePdfPath = "high_res_page.pdf";   // PDF containing the high‑resolution page
        const string outputPdfPath = "tiled_background.pdf";

        // Ensure source file exists
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }

        // Load the source PDF (the page that will be used as background)
        using (Document srcDoc = new Document(sourcePdfPath))
        {
            // Create a new empty PDF document
            using (Document targetDoc = new Document())
            {
                // Add a blank page to the target document
                targetDoc.Pages.Add();

                // Get the first (and only) page of the target document
                Aspose.Pdf.Page targetPage = targetDoc.Pages[1];

                // Create a BackgroundArtifact
                BackgroundArtifact bgArtifact = new BackgroundArtifact();

                // Mark the artifact as a background (placed behind page contents)
                bgArtifact.IsBackground = true;

                // Set the high‑resolution PDF page as the artifact content
                // This will tile the page if the target page is larger; otherwise it will be placed as‑is
                bgArtifact.SetPdfPage(srcDoc.Pages[1]);

                // Optionally adjust alignment or margins if needed
                // bgArtifact.ArtifactHorizontalAlignment = HorizontalAlignment.Center;
                // bgArtifact.ArtifactVerticalAlignment   = VerticalAlignment.Center;

                // Add the artifact to the page's artifact collection
                targetPage.Artifacts.Add(bgArtifact);

                // Save the resulting PDF
                targetDoc.Save(outputPdfPath);
            }
        }

        Console.WriteLine($"Document with tiled background saved to '{outputPdfPath}'.");
    }
}