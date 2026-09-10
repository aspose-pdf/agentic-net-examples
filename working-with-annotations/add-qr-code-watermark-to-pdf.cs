using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Paths for the output PDF and the QR code image.
        const string outputPdfPath = "qr_watermarked.pdf";
        const string qrImagePath   = "qr_code.png";

        // Verify that the QR code image exists.
        if (!File.Exists(qrImagePath))
        {
            Console.Error.WriteLine($"QR code image not found: {qrImagePath}");
            return;
        }

        // Create a new PDF document.
        using (Document doc = new Document())
        {
            // Add a blank page to the document.
            Page page = doc.Pages.Add();

            // Create a WatermarkArtifact instance.
            WatermarkArtifact watermark = new WatermarkArtifact();

            // Set the QR code image for the artifact.
            // The SetImage method accepts a file path or a stream.
            watermark.SetImage(qrImagePath);

            // Position the artifact at the top‑right corner.
            // Alignments are used when Position is not set explicitly.
            watermark.ArtifactHorizontalAlignment = HorizontalAlignment.Right;
            watermark.ArtifactVerticalAlignment   = VerticalAlignment.Top;

            // Optional margins (in points) from the page edges.
            watermark.RightMargin = 20; // 20 points from the right edge
            watermark.TopMargin   = 20; // 20 points from the top edge

            // Ensure the watermark appears above page content.
            watermark.IsBackground = false;

            // Add the artifact to the page's artifact collection.
            page.Artifacts.Add(watermark);

            // Save the PDF document.
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with QR code watermark saved to '{outputPdfPath}'.");
    }
}