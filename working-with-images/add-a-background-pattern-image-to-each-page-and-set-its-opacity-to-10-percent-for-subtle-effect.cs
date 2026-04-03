using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string patternImagePath = "pattern.png";

        // Verify that the source PDF and pattern image exist
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(patternImagePath))
        {
            Console.Error.WriteLine($"Pattern image not found: {patternImagePath}");
            return;
        }

        // Load the existing PDF (document‑disposal‑with‑using rule)
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing rule)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a background artifact for the page
                BackgroundArtifact bgArtifact = new BackgroundArtifact();

                // Set the pattern image (generator‑only background image)
                bgArtifact.SetImage(patternImagePath);

                // Set opacity to 10 percent (0.1)
                bgArtifact.Opacity = 0.1;

                // Ensure the artifact is placed behind page content
                bgArtifact.IsBackground = true;

                // Add the artifact to the page's artifact collection
                page.Artifacts.Add(bgArtifact);
            }

            // Save the modified PDF (save‑to‑non‑pdf‑always‑use‑save‑options rule not needed here)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Background pattern applied to each page and saved as '{outputPath}'.");
    }
}