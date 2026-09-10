using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "watermarked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Define the visual style of the watermark text
            TextState textState = new TextState
            {
                Font          = FontRepository.FindFont("Helvetica"),
                FontSize      = 72,
                ForegroundColor = Aspose.Pdf.Color.FromRgb(0.5, 0.5, 0.5) // light gray
            };

            // Apply the watermark to every page
            foreach (Page page in doc.Pages)
            {
                // Use the full page rectangle as the annotation bounds
                Aspose.Pdf.Rectangle pageRect = page.Rect;

                // Create a WatermarkAnnotation covering the whole page
                WatermarkAnnotation watermark = new WatermarkAnnotation(page, pageRect)
                {
                    Opacity = 0.3 // 30% opacity for semi‑transparent effect
                };

                // Set the watermark text and associate the TextState
                watermark.SetTextAndState(new[] { "CONFIDENTIAL" }, textState);

                // Add the annotation to the page
                page.Annotations.Add(watermark);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}