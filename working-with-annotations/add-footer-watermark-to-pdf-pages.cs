using System;
using System.IO;
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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate pages using 1‑based indexing (Aspose.Pdf uses 1‑based page numbers)
            for (int pageNumber = 1; pageNumber <= doc.Pages.Count; pageNumber++)
            {
                Page page = doc.Pages[pageNumber];

                // Define a rectangle that spans the width of the page and sits at the bottom (footer area)
                // llx = 0, lly = 0, urx = page width, ury = 30 (30 points high footer)
                var footerRect = new Rectangle(0, 0, page.PageInfo.Width, 30);

                // Create a WatermarkAnnotation for this page
                var watermark = new WatermarkAnnotation(page, footerRect)
                {
                    // Unique identifier: page number + GUID
                    Contents = $"Page {pageNumber} – ID: {Guid.NewGuid()}",
                    // Optional styling
                    Opacity = 0.5,
                    Color = Color.Gray,
                    // Align text to the center of the rectangle
                    TextHorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };

                // Add the annotation to the page's annotation collection
                page.Annotations.Add(watermark);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}
