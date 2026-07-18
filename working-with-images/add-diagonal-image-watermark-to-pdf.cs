using System;
using System.IO;
using Aspose.Pdf;               // Core API (Document, Page, ImageStamp, etc.)

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "watermarked.pdf";
        const string watermarkImgPath = "watermark.png";

        // Verify files exist
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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create an ImageStamp – this stamp will render the image as a watermark
            ImageStamp imgStamp = new ImageStamp(watermarkImgPath)
            {
                // Rotate the stamp 45 degrees for diagonal placement
                RotateAngle = 45,

                // Optional visual settings
                Background = false,                     // overlay (true would place behind page content)
                Opacity    = 0.5,                       // semi‑transparent
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center
            };

            // Apply the same stamp to every page in the document
            foreach (Page page in pdfDoc.Pages)
            {
                page.AddStamp(imgStamp);
            }

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPdfPath}'.");
    }
}