using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPath = "watermarked.pdf";
        const string qrImagePath = "qr.png";

        if (!File.Exists(qrImagePath))
        {
            Console.Error.WriteLine($"QR image not found: {qrImagePath}");
            return;
        }

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Create a watermark artifact (parameterless constructor only)
            WatermarkArtifact watermark = new WatermarkArtifact
            {
                Opacity = 0.5,
                // Position the watermark in the top‑right corner
                ArtifactHorizontalAlignment = HorizontalAlignment.Right,
                ArtifactVerticalAlignment = VerticalAlignment.Top
            };

            // Assign the QR code image – Image is read‑only, use SetImage(stream)
            using (FileStream imgStream = File.OpenRead(qrImagePath))
            {
                watermark.SetImage(imgStream);
            }

            // Add the watermark to the page's artifact collection
            page.Artifacts.Add(watermark);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with QR watermark saved to '{outputPath}'.");
    }
}
