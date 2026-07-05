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

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define a rectangle that spans the whole page
            // Fully qualify to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle pageRect = new Aspose.Pdf.Rectangle(
                page.PageInfo.Margin.Left,
                page.PageInfo.Margin.Bottom,
                page.PageInfo.Width - page.PageInfo.Margin.Right,
                page.PageInfo.Height - page.PageInfo.Margin.Top);

            // Create the WatermarkAnnotation
            WatermarkAnnotation watermark = new WatermarkAnnotation(page, pageRect)
            {
                // Set a semi‑transparent color. Gradient fill is not directly supported;
                // a gradient can be simulated via a custom appearance stream, which is
                // beyond the scope of this simple example.
                Color   = Aspose.Pdf.Color.LightGray,
                Opacity = 0.5 // 50 % opacity
            };

            // NOTE: WatermarkAnnotation does not expose a RotateAngle property in the
            // current Aspose.PDF for .NET API. To place the watermark diagonally you would
            // need to create a custom appearance stream with a transformation matrix or
            // draw the watermark using the Graph API. For simplicity, the example keeps
            // the watermark axis‑aligned.

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(watermark);

            // Save the modified PDF (lifecycle rule: save inside using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}
