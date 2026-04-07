using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Count artifacts of type WatermarkArtifact
            int watermarkCount = 0;
            foreach (Artifact artifact in page.Artifacts)
            {
                if (artifact is WatermarkArtifact)
                {
                    watermarkCount++;
                }
            }

            Console.WriteLine($"Watermark artifacts on page 1: {watermarkCount}");
        }
    }
}