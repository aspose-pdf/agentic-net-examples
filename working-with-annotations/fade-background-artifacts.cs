using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_faded.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based indexed
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Iterate over all artifacts on the page
                foreach (Artifact artifact in page.Artifacts)
                {
                    // Apply only to background artifacts
                    if (artifact is BackgroundArtifact bgArtifact)
                    {
                        // Set opacity to 40%
                        bgArtifact.Opacity = 0.4;
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Faded PDF saved to '{outputPath}'.");
    }
}