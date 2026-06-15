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

        // Load the PDF document (using rule: document disposal with using)
        using (Document doc = new Document(inputPath))
        {
            // Iterate over odd‑numbered pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i += 2)
            {
                Page page = doc.Pages[i];
                Aspose.Pdf.Rectangle mediaBox = page.MediaBox;

                double width  = mediaBox.URX - mediaBox.LLX;
                double height = mediaBox.URY - mediaBox.LLY;

                // If the page is landscape, swap width and height to make it portrait
                if (width > height)
                {
                    // Create a new rectangle with swapped dimensions
                    Aspose.Pdf.Rectangle newBox = new Aspose.Pdf.Rectangle(
                        mediaBox.LLX,                     // lower‑left X stays the same
                        mediaBox.LLY,                     // lower‑left Y stays the same
                        mediaBox.LLX + height,            // new upper‑right X (old height)
                        mediaBox.LLY + width              // new upper‑right Y (old width)
                    );

                    page.MediaBox = newBox;
                }
            }

            // Save the modified document (using rule: document disposal with using)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Odd‑page orientation adjusted and saved to '{outputPath}'.");
    }
}