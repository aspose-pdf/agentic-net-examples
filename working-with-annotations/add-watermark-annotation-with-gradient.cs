using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "watermarked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (page indexing is 1‑based)
            Page page = doc.Pages[1];

            // Define a rectangle that covers most of the page.
            // Fully qualify to avoid ambiguity with System.Drawing.Rectangle.
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(
                page.PageInfo.Width * 0.1,   // left
                page.PageInfo.Height * 0.1,  // bottom
                page.PageInfo.Width * 0.9,   // right
                page.PageInfo.Height * 0.9   // top
            );

            // Create the WatermarkAnnotation.
            WatermarkAnnotation watermark = new WatermarkAnnotation(page, rect)
            {
                // Set a semi‑transparent color (the actual gradient will be defined in the appearance stream).
                Color   = Aspose.Pdf.Color.FromRgb(0.2, 0.6, 0.9),
                Opacity = 0.5
                // NOTE: WatermarkAnnotation does not expose a RotateAngle property in the current API.
                // Rotation can be achieved by supplying a custom appearance stream with a transformation matrix.
                // For brevity, the custom appearance (including gradient fill) is omitted here.
            };

            // Add the annotation to the page.
            page.Annotations.Add(watermark);

            // Save the modified PDF (lifecycle rule: save inside using block).
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}
