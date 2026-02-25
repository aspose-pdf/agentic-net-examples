using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace (contains Document, Page, WatermarkArtifact)

class Program
{
    static void Main()
    {
        const string inputPath    = "input.pdf";
        const string outputPath   = "watermarked.pdf";
        const string watermarkPath = "watermark.png";

        // Verify that required files exist
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(watermarkPath))
        {
            Console.Error.WriteLine($"Watermark image not found: {watermarkPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Iterate pages using 1‑based indexing (Aspose.Pdf requirement)
            for (int i = 1; i <= pdfDoc.Pages.Count; i++)
            {
                Page page = pdfDoc.Pages[i];

                // Create a watermark artifact and configure it
                WatermarkArtifact watermark = new WatermarkArtifact();
                watermark.SetImage(watermarkPath);   // Load image without using System.Drawing
                watermark.IsBackground = true;       // Place behind page content
                watermark.Opacity = 0.3;             // Semi‑transparent

                // Add the watermark to the current page
                page.Artifacts.Add(watermark);
            }

            // Save the modified document (PDF format)
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}