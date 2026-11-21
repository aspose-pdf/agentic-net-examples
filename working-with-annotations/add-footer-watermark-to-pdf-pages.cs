using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

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

        // Load the PDF and ensure deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate pages using 1‑based indexing (Aspose.Pdf uses 1‑based page numbers)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Determine page dimensions
                double pageWidth = page.PageInfo.Width;
                double pageHeight = page.PageInfo.Height;

                // Define a rectangle at the bottom of the page (footer area)
                // Here we use a height of 30 points for the watermark
                Aspose.Pdf.Rectangle footerRect = new Aspose.Pdf.Rectangle(
                    llx: 0,
                    lly: 0,
                    urx: pageWidth,
                    ury: 30);

                // Create a unique identifier for this page
                string watermarkText = $"Page {i} - ID: {Guid.NewGuid()}";

                // Create the WatermarkAnnotation
                WatermarkAnnotation watermark = new WatermarkAnnotation(page, footerRect)
                {
                    // Set the displayed text via the Contents property
                    Contents = watermarkText,
                    // Optional visual settings
                    Opacity = 0.5,
                    // Align the text to the center of the rectangle
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                    // Note: The annotation is added to the page's annotation collection.
                    // If a background‑layer watermark is required, consider using a StampAnnotation.
                };

                // Add the annotation to the page
                page.Annotations.Add(watermark);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}
