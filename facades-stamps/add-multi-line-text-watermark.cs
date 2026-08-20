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

        // Load the PDF document
        Document pdfDocument = new Document(inputPath);

        // Define watermark lines with their respective font sizes
        var lines = new (string Text, float FontSize)[]
        {
            ("CONFIDENTIAL", 48f),
            ("DO NOT DISTRIBUTE", 24f)
        };

        // Common text state settings (color, font)
        Font font = FontRepository.FindFont("Helvetica");
        Aspose.Pdf.Color textColor = Aspose.Pdf.Color.Red;

        // Positioning offsets (relative to page centre)
        // Start from the centre and move upward for each subsequent line
        foreach (Page page in pdfDocument.Pages)
        {
            float yOffset = 0f; // reset for each page
            foreach (var (text, size) in lines)
            {
                // Create a TextState for this line
                TextState state = new TextState
                {
                    Font = font,
                    FontSize = size,
                    ForegroundColor = textColor
                };

                // Create a TextStamp with the line text and its TextState
                TextStamp stamp = new TextStamp(text, state)
                {
                    Background = true,          // appear behind page content
                    Opacity = 0.5f,            // semi‑transparent
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    YIndent = yOffset           // offset from centre
                };

                // Add the stamp to the current page
                page.AddStamp(stamp);

                // Move the next line upward (negative Y direction)
                yOffset -= size + 10f; // line height + spacing
            }
        }

        // Save the watermarked PDF
        pdfDocument.Save(outputPath);

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}
