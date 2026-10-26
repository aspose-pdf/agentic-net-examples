using System;
using System.IO;
using System.Collections.Generic;
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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages
            foreach (Page page in doc.Pages)
            {
                // Collect background artifacts first because ArtifactCollection does not support direct removal during enumeration
                var artifactsToRemove = new List<Artifact>();
                foreach (Artifact artifact in page.Artifacts)
                {
                    if (artifact is BackgroundArtifact)
                        artifactsToRemove.Add(artifact);
                }

                // Delete the collected artifacts using ArtifactCollection.Delete
                foreach (Artifact artifact in artifactsToRemove)
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
