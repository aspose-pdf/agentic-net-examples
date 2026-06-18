using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the source high‑resolution PDF and the output document
        const string sourcePdfPath = "high_res_page.pdf";
        const string outputPdfPath = "tiled_background.pdf";

        // Ensure the source file exists
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdfPath}");
            return;
        }

        // Load the source PDF that contains the high‑resolution page to be used as background
        using (Document sourceDoc = new Document(sourcePdfPath))
        {
            // Get the first page (1‑based indexing) from the source document
            Page sourcePage = sourceDoc.Pages[1];

            // Create a new PDF document
            using (Document targetDoc = new Document())
            {
                // Add a new blank page to the target document
                Page targetPage = targetDoc.Pages.Add();

                // Create a BackgroundArtifact instance
                BackgroundArtifact bgArtifact = new BackgroundArtifact();

                // Mark the artifact as background so it appears behind page contents
                bgArtifact.IsBackground = true;

                // Set the high‑resolution PDF page as the artifact content
                bgArtifact.SetPdfPage(sourcePage);

                // Add the artifact to the target page's artifact collection
                targetPage.Artifacts.Add(bgArtifact);

                // Save the resulting document
                targetDoc.Save(outputPdfPath);
            }
        }

        Console.WriteLine($"Document with tiled background saved to '{outputPdfPath}'.");
    }
}