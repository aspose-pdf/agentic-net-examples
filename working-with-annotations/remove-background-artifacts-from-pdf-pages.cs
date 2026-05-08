using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (core API)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            foreach (Page page in doc.Pages)
            {
                // Collect BackgroundArtifact instances to remove
                List<Artifact> toRemove = new List<Artifact>();
                foreach (Artifact artifact in page.Artifacts)
                {
                    if (artifact is BackgroundArtifact)
                        toRemove.Add(artifact);
                }

                // Remove the collected artifacts using ArtifactCollection.Delete
                foreach (Artifact artifact in toRemove)
                {
                    page.Artifacts.Delete(artifact);
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Background artifacts removed. Saved to '{outputPath}'.");
    }
}