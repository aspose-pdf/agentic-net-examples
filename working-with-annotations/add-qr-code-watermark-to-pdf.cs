using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace
using Aspose.Pdf.Drawing;      // For alignment enums (HorizontalAlignment, VerticalAlignment)

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string qrImagePath = "qr.png";          // QR code image file (must exist)
        const string outputPdfPath = "watermarked.pdf";

        // Verify the QR image exists before proceeding
        if (!File.Exists(qrImagePath))
        {
            Console.Error.WriteLine($"QR code image not found: {qrImagePath}");
            return;
        }

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page (first page)
            Page page = doc.Pages.Add();

            // Create a WatermarkArtifact instance
            WatermarkArtifact watermark = new WatermarkArtifact();

            // Set the QR code image for the artifact
            // SetImage accepts a file path or a stream
            watermark.SetImage(qrImagePath);

            // Position the artifact at the top‑right corner using alignment properties
            watermark.ArtifactHorizontalAlignment = HorizontalAlignment.Right;   // Align to right edge
            watermark.ArtifactVerticalAlignment   = VerticalAlignment.Top;      // Align to top edge

            // Optional margins from the edges (in points). Adjust as needed.
            watermark.RightMargin = 20;   // 20 points from the right edge
            watermark.TopMargin   = 20;   // 20 points from the top edge

            // Optional visual settings
            watermark.IsBackground = false;   // Draw over page content
            watermark.Opacity = 0.8;          // Slightly transparent (0..1)

            // Add the artifact to the page's artifact collection
            page.Artifacts.Add(watermark);

            // Save the PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with QR code watermark saved to '{outputPdfPath}'.");
    }
}