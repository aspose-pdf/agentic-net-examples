using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // for FontStyles enum

class WatermarkExample
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "watermarked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a semi‑transparent text stamp that will act as the watermark
            TextStamp stamp = new TextStamp("CONFIDENTIAL")
            {
                // Make the stamp large and centered
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                // Set opacity (0 = fully transparent, 1 = opaque)
                Opacity = 0.25
            };

            // Configure text appearance via the existing TextState instance
            stamp.TextState.FontSize = 72;
            stamp.TextState.FontStyle = FontStyles.Bold;
            stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Gray;

            // Apply the stamp to every page in the document
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(stamp);
            }

            // Save the modified PDF (default Save writes PDF)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}
