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

        // Load the PDF document inside a using block (lifecycle rule)
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("The document contains no pages.");
                return;
            }

            // Get the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Define a rectangle that spans the page diagonally.
            // The rectangle is defined by lower‑left (llx, lly) and upper‑right (urx, ury) coordinates.
            // Here we use the full page size to cover the diagonal.
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(
                page.PageInfo.Margin.Left,
                page.PageInfo.Margin.Bottom,
                page.PageInfo.Width - page.PageInfo.Margin.Right,
                page.PageInfo.Height - page.PageInfo.Margin.Top);

            // Create a WatermarkAnnotation on the page.
            WatermarkAnnotation watermark = new WatermarkAnnotation(page, rect)
            {
                // Set the annotation's opacity (0 = fully transparent, 1 = fully opaque)
                Opacity = 0.5f,

                // Set a base color. A true gradient fill requires a custom appearance stream,
                // which is beyond the scope of this simple example. The color here serves as
                // a placeholder for the gradient effect.
                Color = Aspose.Pdf.Color.LightGray,

                // Optional: set contents (visible when the annotation is selected)
                Contents = "Diagonal Watermark"
            };

            // Add the annotation to the page's annotation collection.
            page.Annotations.Add(watermark);

            // Save the modified document (save rule)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}