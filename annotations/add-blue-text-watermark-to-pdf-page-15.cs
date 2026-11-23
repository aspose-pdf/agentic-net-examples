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
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Verify that page 15 exists (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 15)
            {
                Console.Error.WriteLine("The document contains fewer than 15 pages.");
                return;
            }

            // Get page 15
            Page page = doc.Pages[15];

            // Define a rectangle that covers the whole page (lower‑left x,y = 0,0)
            var rect = new Aspose.Pdf.Rectangle(0, 0, page.PageInfo.Width, page.PageInfo.Height);

            // Create the WatermarkAnnotation – constructor requires page and rectangle
            var watermark = new WatermarkAnnotation(page, rect)
            {
                Opacity = 0.5 // optional – make the watermark semi‑transparent
            };

            // Define the text appearance
            var textState = new TextState
            {
                FontSize = 24,
                ForegroundColor = Aspose.Pdf.Color.Blue
            };

            // Set the watermark text and its style
            watermark.SetTextAndState(new[] { "Confidential" }, textState);

            // Add the annotation to page 15
            page.Annotations.Add(watermark);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}
