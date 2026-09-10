using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string sourcePdfPath = "highres.pdf";
        const string outputPdfPath = "tiled_background.pdf";

        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }

        // Load the PDF that contains the high‑resolution page to be used as background
        using (Document srcDoc = new Document(sourcePdfPath))
        {
            // Aspose.Pdf uses 1‑based page indexing
            Page srcPage = srcDoc.Pages[1];

            // Create a new blank document
            using (Document newDoc = new Document())
            {
                // Add a blank page; optionally match the size of the source page
                Page newPage = newDoc.Pages.Add();
                newPage.SetPageSize(srcPage.MediaBox.Width, srcPage.MediaBox.Height);

                // Create a BackgroundArtifact
                BackgroundArtifact bgArtifact = new BackgroundArtifact();

                // Set the high‑resolution PDF page as the artifact content
                bgArtifact.SetPdfPage(srcPage);

                // Render the artifact behind page contents
                bgArtifact.IsBackground = true;

                // Optional: set opacity (0.0f – fully transparent, 1.0f – fully opaque)
                bgArtifact.Opacity = 1.0f;

                // Add the artifact to the page's artifact collection
                newPage.Artifacts.Add(bgArtifact);

                // Save the new document with the tiled background
                newDoc.Save(outputPdfPath);
            }
        }

        Console.WriteLine($"Document saved to '{outputPdfPath}'.");
    }
}