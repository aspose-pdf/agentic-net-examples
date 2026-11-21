using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Operators;
using Aspose.Pdf.Drawing; // ImageStamp lives here

class ReplaceVectorWithRaster
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string rasterImg = "raster.jpg"; // raster image to insert

        if (!File.Exists(inputPdf) || !File.Exists(rasterImg))
        {
            Console.Error.WriteLine("Input PDF or raster image not found.");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdf))
            {
                // Assume the vector graphic is on the first page (pages are 1‑based)
                Page page = doc.Pages[1];

                // ------------------------------------------------------------
                // 1. Locate and remove the vector graphic operators.
                //    Here we remove the first XObject‑painting operator (Do).
                // ------------------------------------------------------------
                OperatorCollection ops = page.Contents;
                // Iterate backwards because Delete uses a 1‑based index
                for (int i = ops.Count; i >= 1; i--)
                {
                    if (ops[i] is Do)
                    {
                        ops.Delete(i);
                        // Stop after removing the first matching operator.
                        break;
                    }
                }

                // ------------------------------------------------------------
                // 2. Insert the raster image at the same location.
                //    Replace the rectangle with the actual coordinates of the
                //    removed vector graphic if they are known.
                // ------------------------------------------------------------
                // Example rectangle (lower‑left x, lower‑left y, upper‑right x, upper‑right y)
                Aspose.Pdf.Rectangle imgRect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);
                double width = imgRect.URX - imgRect.LLX;
                double height = imgRect.URY - imgRect.LLY;

                ImageStamp imgStamp = new ImageStamp(rasterImg)
                {
                    Background = false,          // draw over existing content
                    Width = width,
                    Height = height,
                    XIndent = imgRect.LLX,       // horizontal offset from the left edge
                    YIndent = imgRect.LLY        // vertical offset from the bottom edge
                };

                // Add the stamp to the page. This inserts the raster image into
                // the page content stream while preserving all other content.
                page.AddStamp(imgStamp);

                // ------------------------------------------------------------
                // 3. Save the modified PDF.
                // ------------------------------------------------------------
                doc.Save(outputPdf);
            }

            Console.WriteLine($"Vector graphic replaced with raster image. Saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
