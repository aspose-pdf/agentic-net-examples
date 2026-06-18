using System;
using System.IO;
using Aspose.Pdf;               // Core API
using Aspose.Pdf.Facades;      // For any facade usage if needed (not used here)

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_background.pdf";
        const string imagePath  = "background.png";   // Path to the background image

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

        // Load the existing PDF document (using the lifecycle rule for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate pages using 1‑based indexing (global rule)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a background artifact, set the image and opacity (30 %)
                BackgroundArtifact bgArtifact = new BackgroundArtifact();
                bgArtifact.SetImage(imagePath);   // Load image from file
                bgArtifact.Opacity = 0.3;         // 30 % opacity (range 0..1)
                bgArtifact.IsBackground = true;  // Ensure it is placed behind page content

                // Add the artifact to the page
                page.Artifacts.Add(bgArtifact);
            }

            // Save the modified PDF (using the lifecycle rule)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with background image to '{outputPath}'.");
    }
}