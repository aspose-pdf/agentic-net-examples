using System;
using System.IO;
using Aspose.Pdf;

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

        // Load the PDF document inside a using block (ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing; get the first page
            Page page = doc.Pages[1];

            // Count only WatermarkArtifact instances on this page
            int watermarkCount = 0;
            foreach (Artifact artifact in page.Artifacts)
            {
                if (artifact is WatermarkArtifact)
                {
                    watermarkCount++;
                }
            }

            Console.WriteLine($"Watermark artifacts on page 1: {watermarkCount}");

            // Save the (unchanged) document to a new file
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}