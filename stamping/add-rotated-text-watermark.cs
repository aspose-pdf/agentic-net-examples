using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // required for FontRepository and TextState

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

        // Load the source PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a text stamp that will act as a slanted watermark
            TextStamp textStamp = new TextStamp("CONFIDENTIAL");
            // Configure the TextState (TextState is read‑only, so we modify the instance it returns)
            textStamp.TextState.Font = FontRepository.FindFont("Arial");
            textStamp.TextState.FontSize = 72;
            textStamp.TextState.ForegroundColor = Aspose.Pdf.Color.Gray;
            // Make the stamp semi‑transparent
            textStamp.Opacity = 0.3;
            // Rotate 30 degrees to achieve the slanted effect
            textStamp.RotateAngle = 30;
            // Center the stamp on each page
            textStamp.HorizontalAlignment = HorizontalAlignment.Center;
            textStamp.VerticalAlignment = VerticalAlignment.Center;
            // Place the stamp in front of page content (Background = false)
            textStamp.Background = false;

            // Apply the stamp to every page individually (Page.AddStamp, not PageCollection)
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(textStamp);
            }

            // Save the modified PDF; the Document is still alive until Save completes
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}
