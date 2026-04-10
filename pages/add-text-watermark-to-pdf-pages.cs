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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Prepare text styling for the watermark
            TextState textState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica"),
                FontSize = 72,
                ForegroundColor = Aspose.Pdf.Color.Red
            };

            // Add the watermark artifact to each page
            foreach (Page page in doc.Pages)
            {
                // Create a new watermark artifact for the current page
                WatermarkArtifact watermark = new WatermarkArtifact();

                // Set the watermark text and its visual properties
                watermark.Text = "CONFIDENTIAL";
                watermark.TextState = textState;

                // Position the watermark (you can adjust the coordinates as needed)
                // Note: Position uses Aspose.Pdf.Point, not Aspose.Pdf.Text.Position
                watermark.Position = new Aspose.Pdf.Point(100, 400);

                // Make the watermark semi‑transparent and place it behind page content
                watermark.Opacity = 0.3;
                watermark.IsBackground = true;

                // Add the artifact to the page's artifact collection
                page.Artifacts.Add(watermark);
            }

            // Save the modified document (PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}