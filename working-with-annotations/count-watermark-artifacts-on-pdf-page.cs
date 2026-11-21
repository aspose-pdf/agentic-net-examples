using System;
using System.IO;
using System.Linq;
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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            int totalWatermarks = 0;

            // Pages are 1‑based in Aspose.Pdf
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Count only WatermarkArtifact instances on the page
                int pageWatermarks = page.Artifacts.OfType<WatermarkArtifact>().Count();

                Console.WriteLine($"Page {i}: {pageWatermarks} watermark artifact(s)");
                totalWatermarks += pageWatermarks;
            }

            Console.WriteLine($"Total watermark artifacts in document: {totalWatermarks}");
        }
    }
}