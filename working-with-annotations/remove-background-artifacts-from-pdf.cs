using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_no_background_artifacts.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages
            foreach (Page page in doc.Pages)
            {
                // Collect BackgroundArtifact objects to remove (cannot modify collection while iterating)
                List<Artifact> artifactsToRemove = new List<Artifact>();

                foreach (Artifact artifact in page.Artifacts)
                {
                    if (artifact is BackgroundArtifact)
                        artifactsToRemove.Add(artifact);
                }

                // Remove the collected artifacts from the page using the correct Delete method
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
