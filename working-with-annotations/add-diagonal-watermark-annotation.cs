using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define a rectangle that covers the whole page.
            double llx = 0;
            double lly = 0;
            double urx = page.PageInfo.Width;
            double ury = page.PageInfo.Height;
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

            // Create a WatermarkAnnotation positioned on the page.
            // NOTE: The modern Aspose.Pdf API does not expose a RotateAngle property on WatermarkAnnotation.
            // To achieve a diagonal appearance we set the rectangle to span the page and rely on the
            // annotation's internal rotation (if needed) via the GraphInfo.TransformationMatrix – which is
            // not available. Therefore we approximate the diagonal effect by using a large rectangle that
            // visually covers the page.
            // Gradient fill is not directly supported by WatermarkAnnotation; we simulate it with a
            // semi‑transparent color that can be changed to any desired shade.
            WatermarkAnnotation watermark = new WatermarkAnnotation(page, rect)
            {
                Opacity = 0.5,                                 // semi‑transparent
                Color   = Aspose.Pdf.Color.FromArgb(128, 255, 0, 0) // placeholder for gradient (red with 50% opacity)
                // RotateAngle property is not available in the current API version.
            };

            // Add the annotation to the page's annotation collection.
            page.Annotations.Add(watermark);

            // Save the modified PDF (lifecycle rule: call Save inside using block).
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermark annotation added and saved to '{outputPath}'.");
    }
}
