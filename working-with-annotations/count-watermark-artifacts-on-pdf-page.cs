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

        // Open the PDF document (lifecycle rule: wrap in using)
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing
            Page page = doc.Pages[1];

            // Count only WatermarkArtifact instances on the page
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