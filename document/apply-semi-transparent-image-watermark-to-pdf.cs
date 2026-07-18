using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "watermarked.pdf";
        const string imagePath = "logo.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Watermark image not found: {imagePath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Apply the semi‑transparent overlay to each page
            foreach (Page page in doc.Pages)
            {
                // Create a watermark artifact (no Rectangle assignment needed)
                WatermarkArtifact watermark = new WatermarkArtifact();
                watermark.SetImage(imagePath);          // set the image source
                watermark.Opacity = 0.3;                // semi‑transparent (30% opacity)

                // Optional: center the watermark on the page
                watermark.ArtifactHorizontalAlignment = HorizontalAlignment.Center;
                watermark.ArtifactVerticalAlignment   = VerticalAlignment.Center;

                // Add the artifact to the page
                page.Artifacts.Add(watermark);
            }

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPdf}'.");
    }
}