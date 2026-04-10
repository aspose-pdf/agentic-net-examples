using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

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

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Iterate through the artifacts on the page
                foreach (Artifact artifact in page.Artifacts)
                {
                    // Identify WatermarkArtifact instances
                    if (artifact is WatermarkArtifact watermark)
                    {
                        // Set opacity to 50%
                        watermark.Opacity = 0.5;
                    }
                }
            }

            // Save the modified PDF (lifecycle rule: save inside using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermark artifacts opacity updated and saved to '{outputPath}'.");
    }
}