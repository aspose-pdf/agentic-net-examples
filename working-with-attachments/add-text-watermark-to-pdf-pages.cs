using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "watermarked.pdf";
        const string watermarkText = "CONFIDENTIAL";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Add a watermark artifact to each page
            foreach (Page page in doc.Pages)
            {
                WatermarkArtifact watermark = new WatermarkArtifact();
                watermark.Text = watermarkText;                     // Set watermark text
                watermark.Opacity = 0.3;                           // Semi‑transparent
                watermark.IsBackground = false;                    // Place over page content
                watermark.ArtifactHorizontalAlignment = HorizontalAlignment.Center;
                watermark.ArtifactVerticalAlignment   = VerticalAlignment.Center;

                page.Artifacts.Add(watermark);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}