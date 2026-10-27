using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "watermarked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document pdfDoc = new Document(inputPath);

        // Create a watermark artifact
        WatermarkArtifact watermark = new WatermarkArtifact();

        // Prepare the text (multi‑line) and its visual style
        string[] lines = { "Confidential", "Do Not Distribute", "© 2026 Company" };
        string watermarkText = string.Join("\n", lines); // newline separates the lines

        TextState textState = new TextState
        {
            Font = FontRepository.FindFont("Arial"),
            FontSize = 36,
            ForegroundColor = Color.Gray
        };

        // Apply the text and style to the artifact
        watermark.SetTextAndState(watermarkText, textState);

        // Configure appearance of the watermark
        watermark.ArtifactHorizontalAlignment = HorizontalAlignment.Center; // center horizontally
        watermark.ArtifactVerticalAlignment   = VerticalAlignment.Center;   // center vertically
        watermark.Rotation = 45;               // rotate 45 degrees
        watermark.Opacity = 0.5f;              // semi‑transparent
        watermark.IsBackground = true;        // render as background (watermark)

        // Add the watermark to every page of the document
        foreach (Page page in pdfDoc.Pages)
        {
            page.Artifacts.Add(watermark);
        }

        // Save the watermarked PDF
        pdfDoc.Save(outputPath);

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}
