using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string outputPdfPath = "output.pdf";
        const string qrImagePath   = "qr.png"; // QR code image file

        // Ensure the QR image exists
        if (!File.Exists(qrImagePath))
        {
            Console.Error.WriteLine($"QR image not found: {qrImagePath}");
            return;
        }

        // Create a new PDF document and add a single blank page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Create a WatermarkArtifact instance
            WatermarkArtifact watermark = new WatermarkArtifact();

            // Set the QR code image for the artifact
            watermark.SetImage(qrImagePath);

            // Position the artifact at the top‑right corner
            // Alignments are used when Position is not set explicitly
            watermark.ArtifactHorizontalAlignment = HorizontalAlignment.Right;
            watermark.ArtifactVerticalAlignment   = VerticalAlignment.Top;

            // Optional: set margins from the page edges (e.g., 20 points)
            watermark.RightMargin = 20;
            watermark.TopMargin   = 20;

            // Add the artifact to the page
            page.Artifacts.Add(watermark);

            // Save the PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with QR code watermark saved to '{outputPdfPath}'.");
    }
}