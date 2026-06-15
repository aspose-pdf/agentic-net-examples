using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // for HorizontalAlignment / VerticalAlignment enums if needed

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

        // Create a new PDF document and add a single blank page
        using (Document doc = new Document())
        {
            // Add a page (Aspose.Pdf uses 1‑based indexing)
            doc.Pages.Add();
            Page page = doc.Pages[1];

            // Create a WatermarkArtifact
            WatermarkArtifact watermark = new WatermarkArtifact();

            // Set the QR code image for the artifact
            watermark.SetImage(qrImagePath);

            // Position the artifact at the top‑right corner
            // Alignments are ignored if Position is set, so we use alignments + margins
            watermark.ArtifactHorizontalAlignment = HorizontalAlignment.Right;
            watermark.ArtifactVerticalAlignment   = VerticalAlignment.Top;

            // Optional margins from the page edges (in points)
            watermark.RightMargin = 10; // distance from right edge
            watermark.TopMargin   = 10; // distance from top edge

            // Ensure the watermark is drawn over page content
            watermark.IsBackground = false;

            // Add the artifact to the page
            page.Artifacts.Add(watermark);

            // Save the PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with QR code watermark saved to '{outputPdfPath}'.");
    }
}