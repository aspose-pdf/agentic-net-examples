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
        const string watermarkText = "CONFIDENTIAL";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Define a TextState that controls font, size, color, etc.
            TextState textState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica"),
                FontSize = 72,
                ForegroundColor = Color.Gray,
                FontStyle = FontStyles.Bold
            };

            // Apply the watermark to every page
            foreach (Page page in doc.Pages)
            {
                // Rectangle covering the whole page
                Aspose.Pdf.Rectangle pageRect = new Aspose.Pdf.Rectangle(
                    0,
                    0,
                    page.PageInfo.Width,
                    page.PageInfo.Height);

                // Create the watermark annotation
                WatermarkAnnotation watermark = new WatermarkAnnotation(page, pageRect)
                {
                    Opacity = 0.3, // semi‑transparent (0 = fully transparent, 1 = opaque)
                    TextHorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };

                // Set the watermark text and associate the TextState
                watermark.SetTextAndState(new[] { watermarkText }, textState);

                // Add the annotation to the page
                page.Annotations.Add(watermark);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}