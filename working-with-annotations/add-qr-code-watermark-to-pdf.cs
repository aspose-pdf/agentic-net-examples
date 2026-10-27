using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string outputPdfPath = "WatermarkedQR.pdf";
        const string qrImagePath    = "qr_code.png";   // QR code image file

        if (!File.Exists(qrImagePath))
        {
            Console.Error.WriteLine($"QR code image not found: {qrImagePath}");
            return;
        }

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Margin from the page edges (points)
            const double margin = 20.0;

            // Create the watermark artifact
            WatermarkArtifact watermark = new WatermarkArtifact();
            // Assign the QR code image via SetImage (Image is read‑only)
            using (FileStream imgStream = File.OpenRead(qrImagePath))
            {
                watermark.SetImage(imgStream);
            }
            // Position it at the top‑right corner
            watermark.ArtifactHorizontalAlignment = HorizontalAlignment.Right;
            watermark.ArtifactVerticalAlignment   = VerticalAlignment.Top;
            watermark.RightMargin = margin;
            watermark.TopMargin   = margin;
            // Optional: make it slightly transparent
            watermark.Opacity = 0.8;

            // Add the watermark to the page's artifacts collection
            page.Artifacts.Add(watermark);

            // Save the PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with QR code watermark saved to '{outputPdfPath}'.");
    }
}
