using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string highResPdfPath = "highres.pdf";
        const string outputPdfPath   = "tiled_background.pdf";

        if (!File.Exists(highResPdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {highResPdfPath}");
            return;
        }

        // Load the high‑resolution PDF that will supply the background page
        using (Document sourceDoc = new Document(highResPdfPath))
        {
            // Aspose.Pdf uses 1‑based page indexing
            Page sourcePage = sourceDoc.Pages[1];

            // Create a new empty PDF document
            using (Document targetDoc = new Document())
            {
                // Add a few pages to demonstrate the tiled background
                for (int i = 0; i < 3; i++)
                {
                    targetDoc.Pages.Add();
                }

                // Apply a BackgroundArtifact to each page using the source page
                for (int pageNum = 1; pageNum <= targetDoc.Pages.Count; pageNum++)
                {
                    Page page = targetDoc.Pages[pageNum];

                    // BackgroundArtifact has only a parameterless constructor.
                    // Use SetPdfPage to copy the high‑resolution page as the background.
                    BackgroundArtifact bg = new BackgroundArtifact();
                    bg.SetPdfPage(sourcePage);
                    // Optional: adjust opacity if a lighter effect is desired.
                    // bg.Opacity = 0.8;
                    page.Artifacts.Add(bg);
                }

                // Save the resulting PDF
                targetDoc.Save(outputPdfPath);
            }
        }

        Console.WriteLine($"Document with tiled background saved to '{outputPdfPath}'.");
    }
}
