using System;
using System.IO;
using Aspose.Pdf;
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

        // Open the source PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TextStamp that will serve as the watermark
            TextStamp watermark = new TextStamp("CONFIDENTIAL")
            {
                // Center the stamp on each page
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center,

                // Semi‑transparent fill (30% opacity)
                Opacity = 0.3f
                // RotateAngle = 45f   // optional rotation
            };

            // TextState is read‑only – modify the existing instance instead of assigning a new one
            watermark.TextState.Font = FontRepository.FindFont("Helvetica-Bold"); // bold font simulates outline
            watermark.TextState.FontSize = 72;
            watermark.TextState.ForegroundColor = Color.FromRgb(0.6, 0.6, 0.6);
            // Aspose.Pdf does not expose StrokeColor/IsStroke for TextStamp; using a bold font provides a visual outline effect.

            // Apply the watermark to every page (1‑based indexing is handled by the foreach)
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(watermark);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}
