using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the source PDF, watermark image, and output PDF
        const string inputPdfPath = "input.pdf";
        const string watermarkImagePath = "watermark.png";
        const string outputPdfPath = "output.pdf";

        // Opacity can be read from a configuration source; here we set it directly
        double watermarkOpacity = 0.3; // Range: 0.0 (transparent) to 1.0 (opaque)

        // Validate required files
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(watermarkImagePath))
        {
            Console.Error.WriteLine($"Watermark image not found: {watermarkImagePath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create an ImageStamp from the watermark image
            ImageStamp stamp = new ImageStamp(watermarkImagePath);
            stamp.Opacity = watermarkOpacity;               // Set semi‑transparent opacity
            stamp.Background = false;                       // Place on top of page content
            stamp.HorizontalAlignment = HorizontalAlignment.Center;
            stamp.VerticalAlignment = VerticalAlignment.Center;

            // Apply the stamp to every page
            foreach (Page page in pdfDoc.Pages)
            {
                page.AddStamp(stamp);
            }

            // Save the watermarked PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPdfPath}'.");
    }
}