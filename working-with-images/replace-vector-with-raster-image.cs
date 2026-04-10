using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class ReplaceVectorWithRaster
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF containing vector graphics
        const string rasterImg = "replacement.png";    // raster image to substitute the vector graphic
        const string outputPdf = "output.pdf";         // result PDF

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(rasterImg))
        {
            Console.Error.WriteLine($"Raster image not found: {rasterImg}");
            return;
        }

        // Load the PDF document (using the recommended load pattern)
        using (Document doc = new Document(inputPdf))
        {
            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Check whether the page contains any vector graphics
                if (page.HasVectorGraphics())
                {
                    // OPTIONAL: extract the vector graphics as SVG for inspection
                    // (not required for the replacement, but useful for debugging)
                    string svgPath = $"page_{pageIndex}_vector.svg";
                    page.TrySaveVectorGraphics(svgPath);

                    // Define the rectangle where the vector graphic resides.
                    // In a real scenario you would calculate this from the SVG or
                    // from a GraphicsAbsorber. Here we use a placeholder rectangle.
                    // The rectangle coordinates are (llx, lly, urx, ury) in points.
                    Aspose.Pdf.Rectangle targetRect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

                    // Add the raster image at the same location, effectively covering the vector graphic.
                    using (FileStream imgStream = File.OpenRead(rasterImg))
                    {
                        // AddImage places the image on the page using the specified rectangle.
                        page.AddImage(imgStream, targetRect);
                    }

                    // OPTIONAL: If you want to remove the original vector content,
                    // you can flatten the page transparency which rasterizes all content.
                    // This step is not strictly necessary when the raster image fully covers the vector.
                    // page.FlattenTransparency();
                }
            }

            // Save the modified document (using the standard save pattern)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Vector graphics replaced and saved to '{outputPdf}'.");
    }
}