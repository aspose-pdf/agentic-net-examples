using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const int pageNumber = 1; // 1‑based page index

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: wrap in using)
        using (Document doc = new Document(inputPath))
        {
            // Validate page number (pages are 1‑based)
            if (pageNumber < 1 || pageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine("Invalid page number.");
                return;
            }

            Page page = doc.Pages[pageNumber];

            // Count only WatermarkArtifact instances on the page
            int watermarkCount = page.Artifacts
                                      .OfType<WatermarkArtifact>()
                                      .Count();

            Console.WriteLine($"Page {pageNumber} contains {watermarkCount} watermark artifact(s).");
        }
    }
}