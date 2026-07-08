using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";      // source PDF
        const string overlayImg = "overlay.png";   // semi‑transparent PNG to use as watermark
        const string outputPdf = "watermarked.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }
        if (!File.Exists(overlayImg))
        {
            Console.Error.WriteLine($"Overlay image not found: {overlayImg}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Apply the watermark to each page
            foreach (Page page in doc.Pages)
            {
                // Create a watermark artifact (artifact is the preferred way to add an image overlay)
                WatermarkArtifact watermark = new WatermarkArtifact();

                // Set the image for the artifact
                watermark.SetImage(overlayImg);

                // Set desired opacity (0.0 = fully transparent, 1.0 = fully opaque)
                watermark.Opacity = 0.3;   // 30 % opacity for a semi‑transparent effect

                // Optionally position the watermark (centered by default; can adjust via Position if needed)
                // watermark.Position = new Position(100, 500); // example positioning

                // Add the artifact to the page's artifact collection
                page.Artifacts.Add(watermark);
            }

            // Save the modified PDF (lifecycle rule: save inside using block)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPdf}'.");
    }
}