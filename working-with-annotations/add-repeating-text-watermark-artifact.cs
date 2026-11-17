using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Annotations; // required for WatermarkArtifact

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
            // Iterate over each page and add a watermark artifact
            foreach (Page page in doc.Pages)
            {
                WatermarkArtifact watermark = new WatermarkArtifact
                {
                    // Text to display
                    Text = "CONFIDENTIAL",

                    // Appearance settings
                    Opacity = 0.2,               // semi‑transparent
                    Rotation = 45,               // diagonal
                    IsBackground = true         // appear behind page content
                    // NOTE: The current Aspose.PDF version does not expose an IsRepeat property.
                    // If a repeating watermark is required, the library would need to be updated
                    // to a version that includes this member, or the watermark must be added
                    // manually multiple times. For simplicity, we add a single watermark per page.
                };

                // Font, size and color are defined via TextState
                watermark.TextState = new TextState
                {
                    Font = FontRepository.FindFont("Arial"),
                    FontSize = 72,
                    ForegroundColor = Color.FromRgb(0.8, 0.8, 0.8) // light gray
                };

                // Add the watermark to the current page's artifacts collection
                page.Artifacts.Add(watermark);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}
