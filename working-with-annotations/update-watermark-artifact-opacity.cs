using System;
using System.IO;
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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Examine each artifact on the page
                foreach (Artifact artifact in page.Artifacts)
                {
                    // Identify Watermark artifacts and set their opacity to 50%
                    if (artifact is WatermarkArtifact watermark)
                    {
                        watermark.Opacity = 0.5f; // 0.0 (transparent) to 1.0 (opaque)
                    }
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermark opacity updated and saved to '{outputPath}'.");
    }
}
