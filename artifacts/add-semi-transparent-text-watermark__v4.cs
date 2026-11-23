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

        // Load the PDF using the core Document API
        Document doc = new Document(inputPath);

        // Configure the watermark artifact (core API – no Facades)
        foreach (Page page in doc.Pages)
        {
            var watermark = new WatermarkArtifact
            {
                Text = "CONFIDENTIAL",
                // Text styling is provided via TextState
                TextState = new TextState
                {
                    FontSize = 72,
                    // Gray color (0.5,0.5,0.5) – values are doubles in the range 0..1
                    ForegroundColor = Color.FromRgb(0.5, 0.5, 0.5),
                    // Choose a font that exists on the system or embed one
                    Font = FontRepository.FindFont("Arial")
                },
                Rotation = 45,          // diagonal orientation
                Opacity = 0.3f,         // 30% opacity (semi‑transparent)
                IsBackground = true    // render as background artifact
            };

            page.Artifacts.Add(watermark);
        }

        // Save the watermarked PDF
        doc.Save(outputPath);

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}
