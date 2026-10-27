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

        // Load the PDF document with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Count only WatermarkArtifact objects on the page
                int watermarkCount = page.Artifacts
                                          .OfType<WatermarkArtifact>()
                                          .Count();

                Console.WriteLine($"Page {i}: {watermarkCount} watermark artifact(s)");
            }
        }
    }
}