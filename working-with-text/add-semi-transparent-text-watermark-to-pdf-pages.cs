using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "watermarked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Use the page rectangle as the watermark bounds
                Aspose.Pdf.Rectangle rect = page.Rect;

                // Create a WatermarkAnnotation that covers the whole page
                WatermarkAnnotation watermark = new WatermarkAnnotation(page, rect);

                // Set semi‑transparent opacity (0.0 = fully transparent, 1.0 = opaque)
                watermark.Opacity = 0.3;

                // Define text appearance via TextState (color + font size)
                // Fully qualify System.Drawing.Color to avoid ambiguity with Aspose.Pdf.Color
                TextState textState = new TextState(System.Drawing.Color.Gray, 48);

                // Set the watermark text (can be multiple lines)
                watermark.SetTextAndState(new string[] { "CONFIDENTIAL" }, textState);

                // Add the annotation to the page
                page.Annotations.Add(watermark);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}
