using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_faded.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            foreach (Page page in doc.Pages)
            {
                // Iterate through all artifacts on the current page
                foreach (Artifact art in page.Artifacts)
                {
                    // Apply opacity change only to background artifacts
                    if (art is BackgroundArtifact bgArtifact)
                    {
                        bgArtifact.Opacity = 0.4; // 40 % opacity for a faded effect
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Faded PDF saved to '{outputPath}'.");
    }
}