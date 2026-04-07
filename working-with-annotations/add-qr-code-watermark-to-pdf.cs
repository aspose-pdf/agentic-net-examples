using System;
using System.IO;
using Aspose.Pdf;                     // Core PDF API
using Aspose.Pdf.Drawing;            // For alignment enums (HorizontalAlignment, VerticalAlignment)

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string outputPdfPath = "qr_watermarked.pdf";
        const string qrImagePath   = "qr_code.png";   // QR code image file (PNG, JPG, etc.)

        // Verify that the QR image exists
        if (!File.Exists(qrImagePath))
        {
            Console.Error.WriteLine($"QR image not found: {qrImagePath}");
            return;
        }

        // Create a new PDF document and add a single blank page
        using (Document doc = new Document())
        {
            // Add a blank page (default size A4)
            Page page = doc.Pages.Add();

            // Create a WatermarkArtifact instance
            WatermarkArtifact watermark = new WatermarkArtifact();

            // Set the QR code image for the artifact
            // Use the overload that accepts a file path
            watermark.SetImage(qrImagePath);

            // Position the artifact at the top‑right corner
            // Alignments are used when Position is not set explicitly
            watermark.ArtifactHorizontalAlignment = HorizontalAlignment.Right;
            watermark.ArtifactVerticalAlignment   = VerticalAlignment.Top;

            // Optional margins from the page edges (in points)
            watermark.RightMargin = 20;   // 20 points from the right edge
            watermark.TopMargin   = 20;   // 20 points from the top edge

            // Add the artifact to the page's artifact collection
            page.Artifacts.Add(watermark);

            // Save the PDF – Document.Save(string) writes a PDF regardless of extension
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with QR code watermark saved to '{outputPdfPath}'.");
    }
}