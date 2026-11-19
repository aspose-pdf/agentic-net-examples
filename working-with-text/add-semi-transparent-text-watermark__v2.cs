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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a semi‑transparent text stamp to serve as the watermark
            TextStamp watermark = new TextStamp("CONFIDENTIAL")
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center,
                Opacity = 0.3f // 0 = fully transparent, 1 = fully opaque
                // Rotation = 45, // optional rotation
            };

            // Configure the visual appearance via TextState
            watermark.TextState.Font = FontRepository.FindFont("Arial");
            watermark.TextState.FontSize = 72;
            // Use ForegroundColor instead of the non‑existent FontColor
            watermark.TextState.ForegroundColor = Aspose.Pdf.Color.Gray;
            watermark.TextState.FontStyle = FontStyles.Bold;
            // Opacity is controlled by the stamp itself; no FontOpacity on TextState

            // Apply the watermark to every page in the document
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
