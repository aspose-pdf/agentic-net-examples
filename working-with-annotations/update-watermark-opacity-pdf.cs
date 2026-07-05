using System;
using System.IO;
using Aspose.Pdf; // Core API namespace

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

        // Load the PDF document (using the mandatory lifecycle rule)
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Iterate through the artifacts collection of the page
                foreach (Artifact artifact in page.Artifacts)
                {
                    // Identify WatermarkArtifact instances
                    if (artifact is WatermarkArtifact watermark)
                    {
                        // Optionally batch updates for performance
                        watermark.BeginUpdates();

                        // Set opacity to 50%
                        watermark.Opacity = 0.5;

                        // Commit the changes
                        watermark.SaveUpdates();
                    }
                }
            }

            // Save the modified PDF (using the mandatory lifecycle rule)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermark opacity updated and saved to '{outputPath}'.");
    }
}