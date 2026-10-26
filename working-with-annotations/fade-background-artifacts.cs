using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_faded.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF and ensure deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages
            foreach (Page page in doc.Pages)
            {
                // Iterate over all artifacts on the page
                foreach (Artifact artifact in page.Artifacts)
                {
                    // Apply only to background artifacts
                    if (artifact is BackgroundArtifact bgArtifact)
                    {
                        // Set opacity to 40% (0.4)
                        bgArtifact.Opacity = 0.4f;
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}