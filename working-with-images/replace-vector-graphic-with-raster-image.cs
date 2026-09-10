using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string rasterImg = "raster.png";

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

        // Load the source PDF
        using (Document doc = new Document(inputPdf))
        {
            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Process only pages that contain vector graphics
                if (page.HasVectorGraphics())
                {
                    // Define the rectangle that encloses the vector graphic to be replaced.
                    // Replace the example coordinates with the actual values for your PDF.
                    double llx = 100; // lower‑left X
                    double lly = 200; // lower‑left Y
                    double urx = 300; // upper‑right X
                    double ury = 400; // upper‑right Y
                    Aspose.Pdf.Rectangle targetRect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

                    // Add the raster image over the vector graphic area.
                    using (FileStream imgStream = File.OpenRead(rasterImg))
                    {
                        page.AddImage(imgStream, targetRect);
                    }
                }
            }

            // Optional: flatten transparency to ensure any remaining vector content is rasterized.
            doc.FlattenTransparency();

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Vector graphic replaced with raster image. Saved to '{outputPdf}'.");
    }
}
