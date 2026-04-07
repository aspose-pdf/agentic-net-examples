using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string sourcePdfPath = "high_res_page.pdf";
        const string outputPdfPath = "tiled_background.pdf";

        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdfPath}");
            return;
        }

        // Load the source PDF that contains the high‑resolution page to be used as background
        using (Document sourceDoc = new Document(sourcePdfPath))
        {
            // Create a new empty PDF document
            using (Document newDoc = new Document())
            {
                // Add a blank page to the new document (pages are 1‑based)
                Page newPage = newDoc.Pages.Add();

                // Create a BackgroundArtifact instance
                BackgroundArtifact bgArtifact = new BackgroundArtifact();

                // Mark it as a background element (behind page contents)
                bgArtifact.IsBackground = true;

                // Set the high‑resolution PDF page as the artifact content
                // Use the first page of the source document (adjust index if needed)
                bgArtifact.SetPdfPage(sourceDoc.Pages[1]);

                // Add the artifact to the page's artifact collection
                newPage.Artifacts.Add(bgArtifact);

                // Save the resulting PDF
                newDoc.Save(outputPdfPath);
                Console.WriteLine($"Document saved with tiled background: {outputPdfPath}");
            }
        }
    }
}