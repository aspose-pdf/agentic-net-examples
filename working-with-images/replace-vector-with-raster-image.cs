using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class ReplaceVectorWithRaster
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF containing a vector graphic
        const string outputPdf = "output.pdf";         // result PDF
        const string rasterImg = "replacement.png";    // raster image that will replace the vector graphic

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

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Iterate through all pages (page indexing is 1‑based)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];

                // Check whether the page contains any vector graphics
                if (!page.HasVectorGraphics())
                    continue; // no vector graphics on this page

                // -----------------------------------------------------------------
                // NOTE:
                // Aspose.Pdf does not provide a direct API to delete a specific
                // vector graphic. The common approach is to overlay a raster image
                // on top of the area occupied by the vector graphic. This preserves
                // the original layout while visually replacing the vector content.
                // -----------------------------------------------------------------

                // Define the rectangle where the vector graphic resides.
                // In a real scenario you would obtain these coordinates
                // programmatically (e.g., via a GraphicsAbsorber). Here we use
                // hard‑coded values for illustration.
                // Rectangle constructor: (llx, lly, urx, ury)
                Aspose.Pdf.Rectangle targetRect = new Aspose.Pdf.Rectangle(100, 400, 300, 600);

                // Add the raster image to the page resources and place it
                // at the same rectangle as the vector graphic.
                using (FileStream imgStream = File.OpenRead(rasterImg))
                {
                    // Add the image to the page resources (optional, but useful if the
                    // same image is reused later). The method returns the image name.
                    string imgName = page.Resources.Images.Add(imgStream);

                    // Reset the stream position before the second use.
                    imgStream.Position = 0;

                    // Place the image on the page at the target rectangle.
                    page.AddImage(imgStream, targetRect);
                }

                // OPTIONAL: If you want to make the original vector graphic invisible,
                // you can flatten the page's transparency which rasterizes the whole
                // page content. This step is not required for a simple overlay.
                // page.FlattenTransparency();
            }

            // Save the modified PDF (lifecycle rule: use doc.Save inside the using block)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Vector graphics have been visually replaced with raster image. Output saved to '{outputPdf}'.");
    }
}