using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPath = "three_page_background.pdf";
        const string backgroundImagePath = "background.png";

        if (!File.Exists(backgroundImagePath))
        {
            Console.Error.WriteLine($"Background image not found: {backgroundImagePath}");
            return;
        }

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add three blank pages
            doc.Pages.Add();
            doc.Pages.Add();
            doc.Pages.Add();

            // Attach the same background image to every page using a BackgroundArtifact
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a BackgroundArtifact – parameterless constructor only
                BackgroundArtifact bgArtifact = new BackgroundArtifact();

                // Assign the image via the writable BackgroundImage property (the stream is copied internally)
                using (FileStream imgStream = File.OpenRead(backgroundImagePath))
                {
                    bgArtifact.BackgroundImage = imgStream;
                }

                // Ensure the artifact is rendered behind page content
                bgArtifact.IsBackground = true;

                // Add the artifact to the current page
                page.Artifacts.Add(bgArtifact);
            }

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
