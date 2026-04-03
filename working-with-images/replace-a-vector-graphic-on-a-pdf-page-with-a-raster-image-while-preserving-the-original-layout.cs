using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string rasterImagePath = "replacement.png";

        if (!File.Exists(inputPdf) || !File.Exists(rasterImagePath))
        {
            Console.Error.WriteLine("Input PDF or raster image not found.");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPdf))
        {
            // Choose the page that contains the vector graphic (1‑based index).
            int pageNumber = 1;
            Page page = doc.Pages[pageNumber];

            // Detect whether the page contains vector graphics.
            if (page.HasVectorGraphics())
            {
                // Optional: export the vector graphics to an SVG file for reference.
                string svgPath = $"page{pageNumber}_vector.svg";
                page.TrySaveVectorGraphics(svgPath);
                Console.WriteLine($"Vector graphics saved to {svgPath}");
            }

            // Define the rectangle where the raster image will be placed.
            // Adjust these coordinates to match the bounds of the original vector graphic.
            float llx = 100f; // lower‑left X
            float lly = 100f; // lower‑left Y
            float urx = 300f; // upper‑right X
            float ury = 300f; // upper‑right Y

            // Use PdfFileMend (Facade) to add the raster image onto the page.
            using (PdfFileMend mend = new PdfFileMend())
            {
                // Bind the existing Document instance to the facade.
                mend.BindPdf(doc);

                // Add the raster image to the specified rectangle on the chosen page.
                using (FileStream imgStream = File.OpenRead(rasterImagePath))
                {
                    mend.AddImage(imgStream, pageNumber, llx, lly, urx, ury);
                }

                // No explicit Save call on the facade is required; changes affect the bound Document.
            }

            // Save the modified PDF document.
            doc.Save(outputPdf);
            Console.WriteLine($"Modified PDF saved to '{outputPdf}'.");
        }
    }
}