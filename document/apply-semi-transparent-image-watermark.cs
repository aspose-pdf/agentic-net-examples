using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "watermarked.pdf";
        const string imagePath = "overlay.png";   // semi‑transparent PNG

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Overlay image not found: {imagePath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document doc = new Document(inputPdf))
        {
            // Apply the watermark to each page
            foreach (Page page in doc.Pages)
            {
                // Create a watermark artifact
                WatermarkArtifact watermark = new WatermarkArtifact();

                // Set the image for the watermark (uses the overload that accepts a file path)
                watermark.SetImage(imagePath);

                // Set semi‑transparent opacity (0.0 = fully transparent, 1.0 = opaque)
                watermark.Opacity = 0.3f;   // 30 % opacity

                // Add the artifact to the page (page.Artifacts is the collection for such objects)
                page.Artifacts.Add(watermark);
            }

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPdf}'.");
    }
}