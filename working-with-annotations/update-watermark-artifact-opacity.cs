using System;
using System.IO;
using Aspose.Pdf; // Core PDF API (Document, Page, Artifact, WatermarkArtifact)

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

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate pages using 1‑based indexing (Aspose.Pdf uses 1‑based page numbers)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // The Artifacts collection holds all page artifacts (including watermarks)
                foreach (Artifact artifact in page.Artifacts)
                {
                    // Identify watermark artifacts via type checking (avoids missing enum)
                    if (artifact is WatermarkArtifact)
                    {
                        // Set opacity to 50% (value range 0.0 – 1.0)
                        artifact.Opacity = 0.5f;
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermark opacity updated and saved to '{outputPath}'.");
    }
}
