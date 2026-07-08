using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string watermarkImgPath = "watermark.png";
        const string outputPdfPath = "output_with_watermark_layer.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(watermarkImgPath))
        {
            Console.Error.WriteLine($"Watermark image not found: {watermarkImgPath}");
            return;
        }

        // Load the existing PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Iterate through each page (or target specific pages)
            foreach (Page page in pdfDoc.Pages)
            {
                // Load the watermark image using System.Drawing.Image (fully qualified)
                using (System.Drawing.Image img = System.Drawing.Image.FromFile(watermarkImgPath))
                {
                    // Define the rectangle where the watermark will be placed
                    // (left, bottom, width, height) – adjust as needed
                    Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);

                    // Create the Watermark object with the image and rectangle
                    Watermark wm = new Watermark(img, rect);

                    // Assign the watermark to the current page
                    page.Watermark = wm;
                }

                // Merge all existing layers (including the newly added watermark) into a single layer
                // The new layer will be named "MergedLayer". Optional content group ID can be omitted.
                page.MergeLayers("MergedLayer");
            }

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with watermark layer saved to '{outputPdfPath}'.");
    }
}
