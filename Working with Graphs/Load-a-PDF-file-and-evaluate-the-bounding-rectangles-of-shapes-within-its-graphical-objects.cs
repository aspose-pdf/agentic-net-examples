using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        // Verify the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Check if the page contains any vector graphics
                if (page.HasVectorGraphics())
                {
                    // Calculate the bounding rectangle that encloses all page content
                    Aspose.Pdf.Rectangle bbox = page.CalculateContentBBox();

                    // Output the rectangle coordinates
                    Console.WriteLine(
                        $"Page {i} – Vector graphics bounding box: " +
                        $"LLX={bbox.LLX}, LLY={bbox.LLY}, URX={bbox.URX}, URY={bbox.URY}");
                }
                else
                {
                    Console.WriteLine($"Page {i} – No vector graphics detected.");
                }
            }
        }
    }
}