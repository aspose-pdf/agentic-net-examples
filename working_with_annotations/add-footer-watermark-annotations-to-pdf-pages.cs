using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate pages using 1‑based indexing (Aspose.Pdf requirement)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Define a rectangle positioned at the bottom of the page (footer area)
                // Rectangle(left, bottom, right, top) – values are in points (1/72 inch)
                // Here we place the watermark 50 points above the bottom edge,
                // spanning the width of the page with a height of 30 points.
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(
                    page.Rect.LLX,               // left
                    page.Rect.LLY,               // bottom
                    page.Rect.URX,               // right
                    page.Rect.LLY + 30);         // top (30 points height)

                // Create the WatermarkAnnotation for the current page
                WatermarkAnnotation wm = new WatermarkAnnotation(page, rect)
                {
                    // Use a unique identifier – e.g., "Page X of Y"
                    Contents = $"Page {i} of {doc.Pages.Count}",
                    // Optional visual styling
                    Color = Aspose.Pdf.Color.Gray,
                    Opacity = 0.5f
                };

                // Add the annotation to the page's annotation collection
                page.Annotations.Add(wm);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}