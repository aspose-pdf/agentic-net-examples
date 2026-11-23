using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "output.pdf";
        const string backgroundPath = "background.jpg";

        // Verify required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(backgroundPath))
        {
            Console.Error.WriteLine($"Background image not found: {backgroundPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Ensure the document has at least three pages (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("The PDF contains fewer than 3 pages.");
                return;
            }

            // Get page three
            Page page = doc.Pages[3];

            // Open the background image stream – it must stay open until the document is saved
            using (FileStream bgStream = File.OpenRead(backgroundPath))
            {
                // Create a BackgroundArtifact and assign the image via BackgroundImage property
                BackgroundArtifact bgArtifact = new BackgroundArtifact();
                bgArtifact.BackgroundImage = bgStream; // writable property
                // Optional: set opacity or background color if desired
                // bgArtifact.Opacity = 0.5;

                // Add the artifact to the page's artifact collection
                page.Artifacts.Add(bgArtifact);

                // Save the modified PDF while the stream is still open
                doc.Save(outputPdfPath);
            }
        }

        Console.WriteLine($"Background image added to page 3 and saved as '{outputPdfPath}'.");
    }
}
