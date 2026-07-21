using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_without_background_artifacts.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using the prescribed lifecycle rule)
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing)
            foreach (Page page in doc.Pages)
            {
                // Collect background artifacts first because ArtifactCollection does not support RemoveAt
                var artifacts = page.Artifacts;
                var toRemove = new List<Artifact>();
                foreach (Artifact artifact in artifacts)
                {
                    if (artifact is BackgroundArtifact)
                        toRemove.Add(artifact);
                }
                // Delete the collected artifacts
                foreach (var artifact in toRemove)
                {
                    artifacts.Delete(artifact);
                }
            }

            // Save the modified document (using the prescribed lifecycle rule)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Background artifacts removed. Saved to '{outputPath}'.");
    }
}
