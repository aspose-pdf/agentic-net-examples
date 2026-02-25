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
        const string watermarkText = "CONFIDENTIAL";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate pages using 1‑based indexing (Aspose.Pdf requirement)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a text watermark artifact
                WatermarkArtifact watermark = new WatermarkArtifact
                {
                    Text = watermarkText,
                    // Configure text appearance
                    TextState = new TextState
                    {
                        Font = FontRepository.FindFont("Arial"),
                        FontSize = 72,
                        ForegroundColor = Aspose.Pdf.Color.FromRgb(0.8, 0.8, 0.8)
                    },
                    // Make the watermark semi‑transparent and place it behind page content
                    Opacity = 0.3,
                    IsBackground = true,
                    // Center the watermark on the page
                    ArtifactHorizontalAlignment = HorizontalAlignment.Center,
                    ArtifactVerticalAlignment   = VerticalAlignment.Center
                };

                // Add the watermark to the current page
                page.Artifacts.Add(watermark);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}