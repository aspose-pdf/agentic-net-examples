using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // for ImageStamp if needed (optional)

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_background.pdf";
        const string imagePath  = "texture.png"; // subtle texture image

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Background image not found: {imagePath}");
            return;
        }

        // Load the source PDF
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a background artifact, set it as background and assign the image
                BackgroundArtifact bgArtifact = new BackgroundArtifact();
                bgArtifact.IsBackground = true;               // place behind page contents
                bgArtifact.SetImage(imagePath);                // load image from file
                bgArtifact.Opacity = 0.5;                     // optional: make the texture subtle

                // Add the artifact to the page
                page.Artifacts.Add(bgArtifact);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with background texture: {outputPath}");
    }
}