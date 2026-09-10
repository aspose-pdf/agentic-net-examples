using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string newImagePath = "newBackground.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        if (!File.Exists(newImagePath))
        {
            Console.Error.WriteLine($"Image file not found: {newImagePath}");
            return;
        }

        // Load the PDF document (1‑based page indexing)
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages
            foreach (Page page in doc.Pages)
            {
                // Iterate over all artifacts on the current page
                foreach (Artifact artifact in page.Artifacts)
                {
                    // Check if the artifact is a background artifact
                    if (artifact is BackgroundArtifact bgArtifact)
                    {
                        // Replace the background image while preserving all other properties
                        // SetImage accepts a file path or a stream; using the path here
                        bgArtifact.SetImage(newImagePath);
                    }
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with updated background images to '{outputPath}'.");
    }
}