using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using statement ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing)
            foreach (Page page in doc.Pages)
            {
                // Collect BackgroundArtifact instances first to avoid modifying the collection while iterating
                List<Artifact> artifactsToRemove = new List<Artifact>();
                foreach (Artifact artifact in page.Artifacts)
                {
                    if (artifact is BackgroundArtifact)
                    {
                        artifactsToRemove.Add(artifact);
                    }
                }

                // Remove each collected BackgroundArtifact from the page
                foreach (Artifact artifact in artifactsToRemove)
                {
                    // Use the Delete method provided by ArtifactCollection instead of Remove
                    page.Artifacts.Delete(artifact);
                    // No explicit Dispose needed; the collection handles cleanup
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"All BackgroundArtifact objects removed. Saved to '{outputPath}'.");
    }
}
