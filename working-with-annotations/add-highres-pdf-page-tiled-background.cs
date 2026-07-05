using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string sourcePdfPath = "highres.pdf";          // PDF containing the high‑resolution page
        const string outputPdfPath = "tiled_background.pdf"; // Resulting document

        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }

        // Load the source PDF (high‑resolution page)
        using (Document sourceDoc = new Document(sourcePdfPath))
        {
            // Use the first page as the background tile
            Aspose.Pdf.Page tilePage = sourceDoc.Pages[1];

            // Create a new empty PDF document
            using (Document targetDoc = new Document())
            {
                // Add a page to the target document (default size)
                Aspose.Pdf.Page newPage = targetDoc.Pages.Add();

                // Create a BackgroundArtifact
                BackgroundArtifact bgArtifact = new BackgroundArtifact();

                // Set the high‑resolution PDF page as the artifact content
                bgArtifact.SetPdfPage(tilePage);

                // Ensure the artifact is drawn behind page contents
                bgArtifact.IsBackground = true;

                // Add the artifact to the page's artifact collection
                newPage.Artifacts.Add(bgArtifact);

                // Save the new document
                targetDoc.Save(outputPdfPath);
            }
        }

        Console.WriteLine($"Document with tiled background saved to '{outputPdfPath}'.");
    }
}