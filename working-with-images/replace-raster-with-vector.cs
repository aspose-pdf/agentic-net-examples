using System;
using System.IO;
using Aspose.Pdf;

class ReplaceRasterWithVector
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_vectorized.pdf";
        const string svgFolder = "ExtractedVectors";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the folder for extracted SVGs exists
        Directory.CreateDirectory(svgFolder);

        // Load the source PDF
        using (Document doc = new Document(inputPdf))
        {
            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // If the page contains vector graphics, extract them as SVG
                if (page.HasVectorGraphics())
                {
                    string svgPath = Path.Combine(svgFolder, $"page_{i}.svg");
                    bool saved = page.TrySaveVectorGraphics(svgPath);
                    if (saved)
                        Console.WriteLine($"Vector graphics of page {i} saved to: {svgPath}");
                }

                // Remove raster images from the page (if any)
                // XImageCollection supports Clear() to drop all image resources
                page.Resources.Images.Clear();

                // Optionally, you can also remove other unused resources
                // to keep the document lightweight.
            }

            // Optimize the document resources after modifications
            doc.OptimizeResources();

            // Save the resulting PDF – it now contains only vector content
            doc.Save(outputPdf);
            Console.WriteLine($"Vector‑only PDF saved to: {outputPdf}");
        }
    }
}