using System;
using System.IO;
using System.Linq;               // For LINQ extensions (OfType, Count)
using Aspose.Pdf;               // Core Aspose.Pdf namespace

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

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing; get the first page
            Page page = doc.Pages[1];

            // Count only artifacts that are of type WatermarkArtifact
            int watermarkCount = page.Artifacts
                                      .OfType<WatermarkArtifact>()
                                      .Count();

            Console.WriteLine($"Watermark artifacts on page 1: {watermarkCount}");
        }
    }
}