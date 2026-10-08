using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing;

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
            // Create a text stamp that will serve as the diagonal watermark
            TextStamp stamp = new TextStamp("CONFIDENTIAL")
            {
                // Rotate 45 degrees to achieve diagonal appearance
                RotateAngle = 45f,

                // Center the stamp on each page
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center,

                // Make the watermark semi‑transparent
                Opacity = 0.3f
            };

            // Configure the visual appearance of the stamp text
            stamp.TextState.FontSize = 72;
            stamp.TextState.FontStyle = FontStyles.Bold;
            stamp.TextState.ForegroundColor = Color.Gray;

            // Apply the stamp to every page individually
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(stamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}