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
            // Iterate over all pages (1‑based indexing is handled by the foreach)
            foreach (Page page in doc.Pages)
            {
                // Iterate over all artifacts on the page
                foreach (Artifact artifact in page.Artifacts)
                {
                    // Change opacity only for background artifacts
                    if (artifact is BackgroundArtifact background)
                    {
                        background.Opacity = 0.4; // 40% opacity
                    }
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved faded PDF to '{outputPath}'.");
    }
}