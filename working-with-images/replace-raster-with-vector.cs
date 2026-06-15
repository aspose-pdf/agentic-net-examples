using System;
using System.IO;
using Aspose.Pdf;

class ReplaceRasterWithVector
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_vectorized.pdf";
        const string svgDir = "ExtractedVectors";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure the directory for extracted SVGs exists
        Directory.CreateDirectory(svgDir);

        // Load the PDF document (using block ensures proper disposal)
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
        {
            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Aspose.Pdf.Page page = doc.Pages[i];

                // If the page contains vector graphics, extract them to an SVG file
                if (page.HasVectorGraphics())
                {
                    string svgPath = Path.Combine(svgDir, $"Page_{i}.svg");
                    // TrySaveVectorGraphics writes the SVG representation of vector content
                    // It does not modify the page; it only creates an external file.
                    page.TrySaveVectorGraphics(svgPath);
                }

                // Remove all raster images from the page resources.
                // This effectively discards raster content; the page will retain only
                // text, vector shapes and any extracted SVGs saved above.
                if (page.Resources.Images.Count > 0)
                {
                    page.Resources.Images.Clear();
                }
            }

            // Save the modified PDF. No explicit SaveOptions are required for PDF output.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Raster images removed and vector graphics extracted to '{svgDir}'.");
        Console.WriteLine($"Resulting PDF saved as '{outputPath}'.");
    }
}
