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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate using 1‑based page indexing (Aspose.Pdf uses 1‑based indexes)
            for (int i = 1; i <= doc.Pages.Count; i += 2) // odd‑numbered pages
            {
                Page page = doc.Pages[i];

                // Current MediaBox dimensions
                double llx = page.MediaBox.LLX;
                double lly = page.MediaBox.LLY;
                double urx = page.MediaBox.URX;
                double ury = page.MediaBox.URY;

                double width  = urx - llx;
                double height = ury - lly;

                // If the page is already portrait, no change needed
                if (height >= width)
                    continue;

                // Swap width and height to make it portrait
                // New upper‑right coordinates: llx + height, lly + width
                Aspose.Pdf.Rectangle newBox = new Aspose.Pdf.Rectangle(
                    llx,
                    lly,
                    llx + height,
                    lly + width
                );

                page.MediaBox = newBox;
            }

            // Save the modified PDF (saving without options writes PDF)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Odd‑page orientation adjusted and saved to '{outputPath}'.");
    }
}