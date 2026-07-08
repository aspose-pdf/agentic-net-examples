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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate pages using 1‑based indexing (Aspose.Pdf requirement)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Define a rectangle positioned at the bottom of the page (footer area)
                // Aspose.Pdf.Rectangle constructor: (llx, lly, urx, ury)
                Aspose.Pdf.Rectangle footerRect = new Aspose.Pdf.Rectangle(
                    page.Rect.LLX,          // left
                    page.Rect.LLY,          // bottom
                    page.Rect.URX,          // right (full page width)
                    page.Rect.LLY + 50);    // top (50 units height)

                // Create a WatermarkAnnotation for the current page
                WatermarkAnnotation watermark = new WatermarkAnnotation(page, footerRect)
                {
                    // Set the visible text – using the page number as a unique identifier
                    Contents = $"Page {i}",
                    // Optional styling
                    Color    = Aspose.Pdf.Color.Gray,
                    Opacity  = 0.5
                };

                // Add the annotation to the page's annotation collection
                page.Annotations.Add(watermark);
            }

            // Save the modified document as PDF (no SaveOptions needed for PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}