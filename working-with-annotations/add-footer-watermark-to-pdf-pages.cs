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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Define a rectangle that spans the full width of the page
                // and sits at the bottom (30 points high)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(
                    page.Rect.LLX,               // left
                    page.Rect.LLY,               // bottom
                    page.Rect.URX,               // right
                    page.Rect.LLY + 30);         // top (30 pts above bottom)

                // Create a WatermarkAnnotation for this page
                WatermarkAnnotation wm = new WatermarkAnnotation(page, rect)
                {
                    // Unique identifier – the page number
                    Contents = $"Page {i}",
                    // Optional visual styling
                    Color = Aspose.Pdf.Color.Gray,
                    Opacity = 0.5
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