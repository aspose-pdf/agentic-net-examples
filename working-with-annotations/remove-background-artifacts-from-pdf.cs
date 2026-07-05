using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf; // Core PDF API (Facades not used per namespace restriction)

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using the prescribed lifecycle rule)
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing internally)
            foreach (Page page in doc.Pages)
            {
                // Collect BackgroundArtifact instances first
                var toRemove = new List<Artifact>();
                foreach (Artifact artifact in page.Artifacts)
                {
                    if (artifact is BackgroundArtifact)
                        toRemove.Add(artifact);
                }

                // Delete the collected artifacts from the page
                foreach (Artifact artifact in toRemove)
                {
                    // Delete removes the artifact from the collection and disposes unmanaged resources
                    page.Artifacts.Delete(artifact);
                }
            }

            // Save the modified PDF (using the prescribed lifecycle rule)
            doc.Save(outputPath);
        }

        Console.WriteLine($"All BackgroundArtifact objects removed. Saved to '{outputPath}'.");
    }
}
