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

        // Load the source PDF
        using (Document doc = new Document(inputPath))
        {
            // Define the visual style of the watermark text
            TextState textState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica"),
                FontSize = 48,
                ForegroundColor = Color.LightGray
            };

            // Add a repeating watermark artifact to each page
            foreach (Page page in doc.Pages)
            {
                WatermarkArtifact watermark = new WatermarkArtifact();

                // Place the artifact behind page contents
                watermark.IsBackground = true;

                // Make the watermark semi‑transparent
                watermark.Opacity = 0.3;

                // Text to repeat across the page
                watermark.Text = "CONFIDENTIAL";

                // Apply the defined text style
                watermark.TextState = textState;

                // Center the text; margins are ignored because Position is not set
                watermark.ArtifactHorizontalAlignment = HorizontalAlignment.Center;
                watermark.ArtifactVerticalAlignment = VerticalAlignment.Center;

                // Attach the artifact to the current page
                page.Artifacts.Add(watermark);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}