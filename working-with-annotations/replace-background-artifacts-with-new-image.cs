using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string newImagePath = "newBackground.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }
        if (!File.Exists(newImagePath))
        {
            Console.Error.WriteLine($"Image file not found: {newImagePath}");
            return;
        }

        // Load the PDF document (using statement ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Iterate over all artifacts on the page
                foreach (Artifact art in page.Artifacts)
                {
                    // Process only background artifacts
                    if (art is BackgroundArtifact bgArtifact)
                    {
                        // Begin batch updates to avoid repeated rendering
                        bgArtifact.BeginUpdates();

                        // Replace the background image while preserving position, margins, etc.
                        bgArtifact.SetImage(newImagePath);

                        // Commit the changes
                        bgArtifact.SaveUpdates();
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}