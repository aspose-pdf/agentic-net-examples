using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // for BackgroundArtifact

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_no_background_artifacts.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing)
            foreach (Page page in doc.Pages)
            {
                // Remove BackgroundArtifact objects safely by iterating backwards
                for (int i = page.Artifacts.Count - 1; i >= 0; i--)
                {
                    Artifact artifact = page.Artifacts[i];
                    if (artifact is BackgroundArtifact)
                    {
                        // ArtifactCollection provides Delete for removal
                        page.Artifacts.Delete(artifact);
                    }
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Background artifacts removed. Saved to '{outputPath}'.");
    }
}
