using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string overlayImagePath = "overlay.png"; // image to use as watermark
        const string outputPdfPath = "watermarked_output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(overlayImagePath))
        {
            Console.Error.WriteLine($"Overlay image not found: {overlayImagePath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Iterate over all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a watermark artifact for the current page
                WatermarkArtifact watermark = new WatermarkArtifact();

                // Set the image for the watermark (can use file path or stream)
                watermark.SetImage(overlayImagePath);

                // Set semi‑transparent opacity (0.0 = fully transparent, 1.0 = opaque)
                watermark.Opacity = 0.3; // 30% opacity

                // Optional: position the watermark.
                // Position is measured from the bottom‑left corner of the page.
                // Here we center the image; adjust X/Y as needed.
                double pageWidth = page.PageInfo.Width;
                double pageHeight = page.PageInfo.Height;
                // Assuming the image size is known (e.g., 200x100 points)
                double imgWidth = 200;
                double imgHeight = 100;
                double posX = (pageWidth - imgWidth) / 2;
                double posY = (pageHeight - imgHeight) / 2;

                // Use Aspose.Pdf.Point (not Aspose.Pdf.Text.Position) for watermark position
                watermark.Position = new Aspose.Pdf.Point(posX, posY);

                // Add the watermark artifact to the page's artifact collection
                page.Artifacts.Add(watermark);
            }

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPdfPath}'.");
    }
}
