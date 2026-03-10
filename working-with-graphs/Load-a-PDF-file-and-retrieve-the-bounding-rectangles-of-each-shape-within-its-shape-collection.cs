using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Calculate the bounding box that encloses all page content.
                // Aspose.Pdf does not expose an individual shape collection,
                // so the closest available information is the content bounding box.
                Aspose.Pdf.Rectangle contentBBox = page.CalculateContentBBox();

                Console.WriteLine($"Page {i} content bounding box:");
                Console.WriteLine($"  Lower‑Left X : {contentBBox.LLX}");
                Console.WriteLine($"  Lower‑Left Y : {contentBBox.LLY}");
                Console.WriteLine($"  Upper‑Right X: {contentBBox.URX}");
                Console.WriteLine($"  Upper‑Right Y: {contentBBox.URY}");
                Console.WriteLine();
            }
        }
    }
}