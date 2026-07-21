using System;
using System.IO;
using Aspose.Pdf;

class ReplaceVectorWithRaster
{
    static void Main()
    {
        // Input PDF containing the vector graphic to be replaced
        const string inputPdf = "input.pdf";
        // Raster image that will replace the vector graphic
        const string rasterImagePath = "replacement.png";
        // Output PDF with the raster image placed over the original vector graphic
        const string outputPdf = "output.pdf";

        // Rectangle coordinates (in points) where the vector graphic resides.
        // Adjust these values to match the exact location and size of the vector graphic.
        // Format: lower‑left‑x, lower‑left‑y, upper‑right‑x, upper‑right‑y
        const double llx = 100; // left
        const double lly = 500; // bottom
        const double urx = 300; // right
        const double ury = 650; // top

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(rasterImagePath))
        {
            Console.Error.WriteLine($"Raster image not found: {rasterImagePath}");
            return;
        }

        // Load the PDF document (using the recommended lifecycle rule)
        using (Document doc = new Document(inputPdf))
        {
            // Work with the first page (1‑based indexing as per Aspose.Pdf)
            Page page = doc.Pages[1];

            // Optional: check whether the page actually contains vector graphics
            if (page.HasVectorGraphics())
            {
                // Create an ImageStamp and position it exactly over the original vector graphic
                ImageStamp imgStamp = new ImageStamp(rasterImagePath)
                {
                    // Set the size of the stamp to match the vector graphic bounds
                    Width = urx - llx,
                    Height = ury - lly,
                    // Set the lower‑left corner position
                    XIndent = llx,
                    YIndent = lly
                };

                // Add the raster image stamp to the page. This overlays the existing vector graphic.
                page.AddStamp(imgStamp);
            }
            else
            {
                Console.WriteLine("No vector graphics detected on the target page.");
            }

            // Save the modified PDF (using the standard Save method; no extra SaveOptions needed)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Raster image placed and PDF saved to '{outputPdf}'.");
    }
}
