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

        // Load the high‑resolution PDF that provides the background page
        using (Document sourceDoc = new Document(sourcePdfPath))
        {
            // Use the first page of the source as the background tile
            Page backgroundPage = sourceDoc.Pages[1];

            // Create a new PDF document that will receive the tiled background
            using (Document targetDoc = new Document())
            {
                // Add a blank page (default size) to the target document
                targetDoc.Pages.Add();

                // Get the page we just added
                Page targetPage = targetDoc.Pages[1];

                // Create a BackgroundArtifact and set the PDF page as its content
                BackgroundArtifact bgArtifact = new BackgroundArtifact();
                bgArtifact.SetPdfPage(backgroundPage);
                bgArtifact.IsBackground = true; // place behind page contents

                // Add the artifact to the page; Aspose.Pdf will tile it automatically
                targetPage.Artifacts.Add(bgArtifact);

                // Save the resulting PDF
                targetDoc.Save(outputPdfPath);
            }
        }

        Console.WriteLine($"Document saved to '{outputPdfPath}'.");
    }
}