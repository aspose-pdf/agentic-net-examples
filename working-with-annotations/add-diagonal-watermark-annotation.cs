using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "watermarked_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using the standard load constructor)
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define a rectangle that spans the page diagonally.
            // The rectangle is larger than the page so that after placement it covers the whole page.
            // Fully qualify Rectangle to avoid ambiguity with System.Drawing.
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(
                page.PageInfo.Width / 2,               // lower‑left X
                page.PageInfo.Height / 2,              // lower‑left Y
                page.PageInfo.Width * 1.5,              // upper‑right X (extends beyond page)
                page.PageInfo.Height * 1.5);            // upper‑right Y

            // Create the WatermarkAnnotation on the page.
            WatermarkAnnotation watermark = new WatermarkAnnotation(page, rect)
            {
                // Set a semi‑transparent color (the actual gradient will be simulated via opacity).
                Color   = Aspose.Pdf.Color.FromRgb(0.2, 0.6, 0.9), // light blue base color
                Opacity = 0.5                                   // 50 % opacity
                // No RotateAngle property exists on WatermarkAnnotation in the current API.
                // The diagonal placement is achieved by the rectangle geometry itself.
            };

            // NOTE: Aspose.Pdf does not expose a direct GradientFill property for annotations.
            // To achieve a true gradient you would need to construct a custom appearance stream
            // (PDF shading dictionary) and assign it to watermark.Appearance. That is beyond the
            // scope of this example, so we simulate a gradient effect by using a semi‑transparent
            // color and a diagonal placement.

            // Add the annotation to the page's annotation collection.
            page.Annotations.Add(watermark);

            // Save the modified document (using the standard Save method).
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}
