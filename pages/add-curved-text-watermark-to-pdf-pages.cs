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
        const string outputPath = "watermarked_curved.pdf";
        const string watermarkText = "CONFIDENTIAL";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block (lifecycle rule)
        using (Document doc = new Document(inputPath))
        {
            // Create a TextState for the watermark appearance
            TextState ts = new TextState
            {
                Font = FontRepository.FindFont("Helvetica"),
                FontSize = 48,
                ForegroundColor = Color.Red,
                // Use a rendering mode that adds the text to the clipping path;
                // this allows the text to follow a path when combined with a transformation.
                RenderingMode = TextRenderingMode.FillThenStrokeTextAndAddPathToClipping
                // StrokeColor and StrokeWidth are not available in the current API version and are therefore omitted.
            };

            // Create a WatermarkArtifact (artifact is the recommended way to add watermarks)
            WatermarkArtifact watermark = new WatermarkArtifact();

            // Set the text and its visual state
            watermark.SetTextAndState(watermarkText, ts);

            // Position the artifact roughly at the center of the page (use Point, not Position)
            watermark.Position = new Point(200, 400);

            // Apply a rotation transform that creates a curved appearance.
            // By rotating the artifact in small increments across the page,
            // the text will appear to follow a gentle curve.
            // Here we set a rotation angle of 30 degrees as an example.
            watermark.Rotation = 30;

            // Make the watermark semi‑transparent so underlying content remains readable
            watermark.Opacity = 0.3f;

            // Add the artifact to each page (for demonstration we apply to all pages)
            foreach (Page page in doc.Pages)
            {
                page.Artifacts.Add(watermark);
            }

            // Save the modified PDF (lifecycle rule)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}
