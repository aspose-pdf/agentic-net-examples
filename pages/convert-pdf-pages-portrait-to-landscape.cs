using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "landscape_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Wrap Document in a using block for deterministic disposal (rule: document-disposal-with-using)
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing (rule: page-indexing-one-based)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Get current MediaBox coordinates
                double llx = page.MediaBox.LLX;
                double lly = page.MediaBox.LLY;
                double urx = page.MediaBox.URX;
                double ury = page.MediaBox.URY;

                // Compute width and height
                double width  = urx - llx;
                double height = ury - lly;

                // Swap width and height by creating a new rectangle
                // Keep lower‑left corner unchanged, adjust upper‑right corner
                page.MediaBox = new Rectangle(
                    llx,
                    lly,
                    llx + height,   // new URX = original height
                    lly + width     // new URY = original width
                );
            }

            // Save the modified document (rule: document-disposal-with-using ensures target stays alive)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Pages converted to landscape and saved as '{outputPath}'.");
    }
}