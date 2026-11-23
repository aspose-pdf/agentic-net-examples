using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string newImagePath = "newBackground.jpg";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(newImagePath))
        {
            Console.Error.WriteLine($"Image file not found: {newImagePath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Aspose.Pdf uses 1‑based page indexing
            if (doc.Pages.Count < 2)
            {
                Console.Error.WriteLine("The document has fewer than 2 pages.");
                return;
            }

            // Get page 2
            Page page = doc.Pages[2];

            // Find an existing BackgroundArtifact, if any
            BackgroundArtifact bgArtifact = null;
            foreach (Artifact art in page.Artifacts)
            {
                if (art is BackgroundArtifact)
                {
                    bgArtifact = (BackgroundArtifact)art;
                    break;
                }
            }

            // If there is no BackgroundArtifact, create one and add it to the page
            if (bgArtifact == null)
            {
                bgArtifact = new BackgroundArtifact();
                page.Artifacts.Add(bgArtifact);
            }

            // Replace the background image with the new image.
            // BackgroundImage expects a Stream; keep the stream open until the document is saved.
            using (FileStream imgStream = File.OpenRead(newImagePath))
            {
                bgArtifact.BackgroundImage = imgStream;
                // Save the modified PDF (Document.Save without SaveOptions always writes PDF)
                doc.Save(outputPdfPath);
            }
        }

        Console.WriteLine($"Background image on page 2 replaced and saved to '{outputPdfPath}'.");
    }
}
