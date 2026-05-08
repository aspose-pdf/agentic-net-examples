using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string qrImagePath = "qr.png";          // QR code image file
        const string outputPdfPath = "watermarked.pdf";

        // Verify QR image exists
        if (!File.Exists(qrImagePath))
        {
            Console.Error.WriteLine($"QR image not found: {qrImagePath}");
            return;
        }

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page (default size A4)
            Page page = doc.Pages.Add();

            // Create a WatermarkArtifact instance
            WatermarkArtifact watermark = new WatermarkArtifact();

            // Set the QR code image for the artifact
            watermark.SetImage(qrImagePath);

            // Position the watermark at the top‑right corner.
            // Use alignment properties and margins for simplicity.
            watermark.ArtifactHorizontalAlignment = HorizontalAlignment.Right;
            watermark.ArtifactVerticalAlignment   = VerticalAlignment.Top;

            // Optional: set margins from the page edges (in points)
            watermark.RightMargin = 20;   // 20 points from the right edge
            watermark.TopMargin   = 20;   // 20 points from the top edge

            // Optionally make the watermark semi‑transparent
            watermark.Opacity = 0.7f;

            // Add the artifact to the page's artifact collection
            page.Artifacts.Add(watermark);

            // Save the PDF (using the standard Save method – no SaveOptions needed)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with QR code watermark saved to '{outputPdfPath}'.");
    }
}