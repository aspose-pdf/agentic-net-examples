using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string imagePath  = "background.png";
        const string outputPath = "output.pdf";

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

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages and add the background image
            foreach (Page page in doc.Pages)
            {
                // Create a background artifact
                BackgroundArtifact bgArtifact = new BackgroundArtifact();

                // Set the image for the artifact (using file path)
                bgArtifact.SetImage(imagePath);

                // Mark the artifact as a background (placed behind page contents)
                bgArtifact.IsBackground = true;

                // Adjust opacity to achieve a subtle shading effect.
                // Blend mode "multiply" is not directly exposed in Aspose.Pdf;
                // using a lower opacity approximates the desired effect.
                bgArtifact.Opacity = 0.5; // 0 = fully transparent, 1 = fully opaque

                // Add the artifact to the page
                page.Artifacts.Add(bgArtifact);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with background image saved to '{outputPath}'.");
    }
}