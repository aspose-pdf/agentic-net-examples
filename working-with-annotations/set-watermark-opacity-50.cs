using System;
using System.IO;
using Aspose.Pdf;               // Core PDF classes
using Aspose.Pdf.Facades;      // Not needed here but kept for completeness

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

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Iterate over all artifacts on the page
                foreach (Artifact artifact in page.Artifacts)
                {
                    // Check if the artifact is a WatermarkArtifact
                    if (artifact is WatermarkArtifact watermark)
                    {
                        // Set opacity to 50%
                        watermark.Opacity = 0.5;
                    }
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermark opacities updated and saved to '{outputPath}'.");
    }
}