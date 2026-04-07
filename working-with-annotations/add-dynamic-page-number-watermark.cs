using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "watermarked_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block (lifecycle rule)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing rule)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Define the rectangle where the watermark will appear
                // Fully qualify to avoid ambiguity with System.Drawing.Rectangle
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

                // Create the WatermarkAnnotation for the current page
                WatermarkAnnotation watermark = new WatermarkAnnotation(page, rect)
                {
                    // Visual styling (Color sets the text color for a watermark)
                    Color = Aspose.Pdf.Color.Black,
                    Opacity = 0.5,
                    // Dynamic page number – set per‑page content
                    Contents = $"Page {i}"
                };

                // Add the annotation to the page
                page.Annotations.Add(watermark);
            }

            // Save the modified PDF (save rule)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}
