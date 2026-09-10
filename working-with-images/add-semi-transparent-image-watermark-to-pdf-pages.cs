using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // for alignment enums if needed

class Program
{
    static void Main()
    {
        // Input PDF, output PDF, and watermark image paths
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "output.pdf";
        const string watermarkImgPath = "watermark.png";

        // Opacity value (0.0 = fully transparent, 1.0 = fully opaque)
        // In a real scenario this could be read from a config file.
        const double watermarkOpacity = 0.35;

        // Validate files
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(watermarkImgPath))
        {
            Console.Error.WriteLine($"Watermark image not found: {watermarkImgPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Iterate over all pages (1‑based indexing)
            foreach (Page page in pdfDoc.Pages)
            {
                // Create an ImageStamp for the watermark image
                ImageStamp imgStamp = new ImageStamp(watermarkImgPath);

                // Set the desired opacity (0..1)
                imgStamp.Opacity = watermarkOpacity;

                // Optional: position the watermark at the center of the page
                imgStamp.Background = false; // draw on top of page content
                imgStamp.HorizontalAlignment = HorizontalAlignment.Center;
                imgStamp.VerticalAlignment   = VerticalAlignment.Center;

                // Add the stamp to the current page
                page.AddStamp(imgStamp);
            }

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPdfPath}'.");
    }
}