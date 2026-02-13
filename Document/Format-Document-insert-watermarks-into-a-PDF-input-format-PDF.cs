using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;

class WatermarkInserter
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_watermarked.pdf";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing PDF document
            Document pdfDocument = new Document(inputPath);

            // Define watermark rectangle (position and size) – here we cover the whole page
            foreach (Page page in pdfDocument.Pages)
            {
                // Use page dimensions to create a rectangle covering the page
                // Fully qualify the Rectangle type to avoid ambiguity with Aspose.Pdf.Drawing.Rectangle
                var rect = new Aspose.Pdf.Rectangle(0, 0, page.PageInfo.Width, page.PageInfo.Height);

                // Create watermark annotation
                WatermarkAnnotation watermark = new WatermarkAnnotation(page, rect)
                {
                    // Optional: set a tooltip
                    Contents = "Confidential",
                    // Set opacity (0.0 to 1.0)
                    Opacity = 0.3,
                    // Set color of the watermark text (if any)
                    Color = Color.Gray
                };

                // Initialize border using the provided rule
                // Border must be initialized after the annotation object
                watermark.Border = new Border(watermark)
                {
                    Style = BorderStyle.Solid,
                    Width = 1
                };

                // Add the watermark annotation to the page
                page.Annotations.Add(watermark);
            }

            // Save the modified PDF
            pdfDocument.Save(outputPath);
            Console.WriteLine($"Watermarked PDF saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}
