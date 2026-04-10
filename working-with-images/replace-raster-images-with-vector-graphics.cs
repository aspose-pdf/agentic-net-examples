using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_vectorized.pdf";
        const string vectorDir  = "VectorGraphics";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure the folder for extracted SVG files exists
        Directory.CreateDirectory(vectorDir);

        // Load the PDF (lifecycle rule: wrap Document in using)
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // -----------------------------------------------------------------
                // 1. Extract existing vector graphics to separate SVG files.
                //    Page.HasVectorGraphics() tells us whether the page contains
                //    path construction operators (i.e., true vector content).
                //    TrySaveVectorGraphics writes those vectors to an SVG file.
                // -----------------------------------------------------------------
                if (page.HasVectorGraphics())
                {
                    string svgPath = Path.Combine(vectorDir, $"page_{i}.svg");
                    bool saved = page.TrySaveVectorGraphics(svgPath);
                    Console.WriteLine(saved
                        ? $"Extracted vector graphics from page {i} to {svgPath}"
                        : $"Failed to extract vector graphics from page {i}");
                }

                // -----------------------------------------------------------------
                // 2. Remove raster images from the page.
                //    Raster images are stored in page.Resources.Images.
                //    Removing them eliminates the raster content, leaving only
                //    the vector graphics extracted above.
                // -----------------------------------------------------------------
                if (page.Resources.Images.Count > 0)
                {
                    // XImageCollection implements ICollection, so Clear() removes all images.
                    page.Resources.Images.Clear();
                    Console.WriteLine($"Removed raster images from page {i}");
                }
            }

            // -----------------------------------------------------------------
            // 3. Optimize the document after modifications.
            //    This removes unused objects, merges duplicate resources, and
            //    compresses streams, resulting in a smaller, cleaner PDF.
            // -----------------------------------------------------------------
            doc.OptimizeResources();

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Vector‑enhanced PDF saved to '{outputPath}'");
    }
}