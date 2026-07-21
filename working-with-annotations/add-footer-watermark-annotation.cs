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
                // Rectangle constructor: (llx, lly, urx, ury)
                // Here we use the full page width and a height of 30 points.
                Aspose.Pdf.Rectangle footerRect = new Aspose.Pdf.Rectangle(
                    page.Rect.LLX,               // left
                    page.Rect.LLY,               // bottom
                    page.Rect.URX,               // right
                    page.Rect.LLY + 30);         // top (30 points above the bottom)

                // Create a WatermarkAnnotation for this page
                WatermarkAnnotation wm = new WatermarkAnnotation(page, footerRect)
                {
                    // Set the visible text of the annotation.
                    // Using the page number as a simple unique identifier.
                    Contents = $"Page {page.Number}",
                    // Optional: set a light gray background color for visibility
                    Color = Aspose.Pdf.Color.LightGray,
                    // Optional: make the annotation semi‑transparent
                    Opacity = 0.5f
                };

                // Add the annotation to the page's annotation collection
                page.Annotations.Add(wm);
            }

            // Save the modified document (PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}