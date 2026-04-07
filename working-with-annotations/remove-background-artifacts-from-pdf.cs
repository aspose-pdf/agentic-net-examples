using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_without_background_artifacts.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (page indexing is 1‑based, but foreach abstracts that)
            foreach (Page page in doc.Pages)
            {
                // Collect BackgroundArtifact instances to remove
                var artifactsToRemove = new List<Artifact>();

                foreach (Artifact artifact in page.Artifacts)
                {
                    if (artifact is BackgroundArtifact)
                        artifactsToRemove.Add(artifact);
                }

                // Remove the collected artifacts from the page.
                // Cast to IList<Artifact> to use the IList.Remove method and avoid the
                // ambiguous CollectionExtensions.Remove overload that expects a dictionary.
                var artifactList = (IList<Artifact>)page.Artifacts;
                foreach (Artifact artifact in artifactsToRemove)
                {
                    artifactList.Remove(artifact);
                }
            }

            // Save the modified document (lifecycle rule: use Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Background artifacts removed. Saved to '{outputPath}'.");
    }
}
