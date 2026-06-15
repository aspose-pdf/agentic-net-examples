using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "watermarked_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing)
            foreach (Page page in doc.Pages)
            {
                // Create a new WatermarkArtifact for the current page
                WatermarkArtifact watermark = new WatermarkArtifact();

                // Place the artifact behind page contents
                watermark.IsBackground = true;

                // Set opacity (0.0 = fully transparent, 1.0 = fully opaque)
                watermark.Opacity = 0.5;

                // Define the repeating text
                watermark.Text = "CONFIDENTIAL";

                // Configure text appearance via TextState
                TextState ts = new TextState
                {
                    // Use a standard font; FontRepository is in Aspose.Pdf.Text
                    Font = FontRepository.FindFont("Helvetica"),
                    FontSize = 48,
                    // Use Aspose.Pdf.Color for cross‑platform compatibility
                    ForegroundColor = Aspose.Pdf.Color.Gray
                };
                watermark.TextState = ts;

                // Optional: rotate the watermark for a diagonal effect
                watermark.Rotation = 45;

                // Add the artifact to the page's artifact collection
                page.Artifacts.Add(watermark);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}