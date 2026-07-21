using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;

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

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Define the pages from which the specific Bates numbering stamp should be removed.
            // Page numbers are 1‑based (Aspose.Pdf uses 1‑based indexing).
            int[] targetPages = { 2, 4, 5 };

            // Define a criterion that identifies the Bates stamp to remove.
            // For example, remove only stamps whose Prefix equals "CONF".
            const string targetPrefix = "CONF";

            foreach (int pageNumber in targetPages)
            {
                // Guard against out‑of‑range page numbers.
                if (pageNumber < 1 || pageNumber > doc.Pages.Count)
                    continue;

                Page page = doc.Pages[pageNumber];

                // Collect the Bates artifacts that match the criterion.
                List<Artifact> artifactsToRemove = new List<Artifact>();
                foreach (Artifact artifact in page.Artifacts)
                {
                    if (artifact is BatesNArtifact bates && bates.Prefix == targetPrefix)
                    {
                        artifactsToRemove.Add(bates);
                    }
                }

                // Remove the identified artifacts from the page.
                foreach (Artifact artifact in artifactsToRemove)
                {
                    // ArtifactCollection provides a Delete method that accepts the artifact instance.
                    page.Artifacts.Delete(artifact);
                }
            }

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Specific Bates numbering stamps removed. Output saved to '{outputPath}'.");
    }
}