using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_background.pdf";
        const string bgImagePath = "texture.png"; // subtle texture image

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }

        if (!File.Exists(bgImagePath))
        {
            Console.Error.WriteLine($"Background image not found: {bgImagePath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a background artifact for the page
                BackgroundArtifact bgArtifact = new BackgroundArtifact();

                // Set the image for the artifact (using the file path)
                bgArtifact.SetImage(bgImagePath);

                // Place the artifact behind page contents
                bgArtifact.IsBackground = true;

                // Set a low opacity to achieve a subtle overlay effect
                bgArtifact.Opacity = 0.3f; // adjust as needed (0..1)

                // Add the artifact to the page's artifact collection
                page.Artifacts.Add(bgArtifact);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with background texture: {outputPath}");
    }
}