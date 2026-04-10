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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Define the visual style of the watermark text
            TextState watermarkState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica"),
                FontSize = 72,
                ForegroundColor = Aspose.Pdf.Color.Gray
            };

            // Apply the watermark to every page (Aspose.Pdf uses 1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a watermark artifact containing the text
                WatermarkArtifact watermark = new WatermarkArtifact
                {
                    Text        = "CONFIDENTIAL",
                    TextState   = watermarkState,
                    Opacity     = 0.3,   // semi‑transparent
                    IsBackground = true   // place behind existing page content
                };

                // Add the artifact to the page
                page.Artifacts.Add(watermark);
            }

            // Save the modified document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}