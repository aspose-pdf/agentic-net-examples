using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace
using Aspose.Pdf.Facades;      // For any facade usage (not needed here but safe)

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_modified.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using the recommended lifecycle pattern)
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Iterate over all artifacts on the page
                foreach (Artifact artifact in page.Artifacts)
                {
                    // Identify WatermarkArtifact instances
                    if (artifact is WatermarkArtifact watermark)
                    {
                        // Optional: batch updates for performance
                        watermark.BeginUpdates();

                        // Set opacity to 50%
                        watermark.Opacity = 0.5;

                        // Commit the changes
                        watermark.SaveUpdates();
                    }
                }
            }

            // Save the modified document (no SaveOptions needed for PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermark artifacts opacity updated and saved to '{outputPath}'.");
    }
}