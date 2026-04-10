using System;
using System.IO;
using Aspose.Pdf;                     // Core PDF API
using Aspose.Pdf.Text;                // For text-related types if needed

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // PDF containing the raster image
        const string outputPath = "output_resized.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];

                // Iterate over all paragraph elements on the page
                for (int i = 1; i <= page.Paragraphs.Count; i++)
                {
                    // The Paragraphs collection can contain different element types.
                    // We are interested only in Image objects.
                    if (page.Paragraphs[i] is Image img)
                    {
                        // Set the desired width and height (in points).
                        // FixWidth / FixHeight override the original image dimensions.
                        img.FixWidth  = 200;   // new width
                        img.FixHeight = 150;   // new height

                        // Optionally, you can also adjust scaling if needed:
                        // img.ImageScale = 1.0; // keep scaling at 100%
                    }
                }
            }

            // Save the modified PDF. No SaveOptions are needed because the output format is PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
    }
}