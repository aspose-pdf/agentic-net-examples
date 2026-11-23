using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // WatermarkArtifact resides in this namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "watermarks_page2.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Verify that the document has at least two pages (1‑based indexing)
            if (doc.Pages.Count < 2)
            {
                Console.Error.WriteLine("The document contains fewer than 2 pages.");
                return;
            }

            // Access page 2 (Aspose.Pdf uses 1‑based page indexing)
            Page page = doc.Pages[2];

            // Open the output text file
            using (StreamWriter writer = new StreamWriter(outputPath, false))
            {
                // Iterate over all artifacts on the page
                foreach (var artifact in page.Artifacts)
                {
                    // Filter only WatermarkArtifact instances
                    if (artifact is WatermarkArtifact watermark)
                    {
                        // Retrieve the rectangle defining the watermark's bounds
                        Aspose.Pdf.Rectangle rect = watermark.Rectangle;

                        // Write the rectangle coordinates to the text file
                        writer.WriteLine(
                            $"WatermarkArtifact: LLX={rect.LLX}, LLY={rect.LLY}, URX={rect.URX}, URY={rect.URY}");
                    }
                }
            }
        }

        Console.WriteLine($"Watermark coordinates have been written to '{outputPath}'.");
    }
}