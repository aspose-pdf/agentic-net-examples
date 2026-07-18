using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing;

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

        // Open the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("The document contains no pages.");
                return;
            }

            // Choose the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Create a watermark artifact (text based)
            WatermarkArtifact watermark = new WatermarkArtifact();

            // Define the visual style of the watermark text
            TextState textState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica"),
                FontSize = 48,
                ForegroundColor = Aspose.Pdf.Color.FromRgb(0.8, 0.2, 0.2), // reddish color
                // Optional: make it semi‑transparent via the artifact's Opacity property
                // (the TextState itself does not have opacity)
            };

            // Set the watermark text and its style
            watermark.SetTextAndState("CONFIDENTIAL", textState);

            // Position the artifact roughly at the centre of the page
            // (Position is a Point; X = left, Y = bottom)
            // Here we place it at (page width/2, page height/2)
            watermark.Position = new Aspose.Pdf.Point(page.PageInfo.Width / 2, page.PageInfo.Height / 2);

            // Rotate the artifact to follow a simple curved appearance.
            // Since Aspose.Pdf does not provide direct path‑text rendering,
            // we simulate a curve by applying a rotation that changes per page.
            // For a single page we set a fixed rotation (e.g., 30 degrees).
            watermark.Rotation = 30; // degrees

            // Make the watermark appear behind the page content
            watermark.IsBackground = true;

            // Add the artifact to the page's artifact collection
            page.Artifacts.Add(watermark);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}