using System;
using System.IO;
using Aspose.Pdf;                     // Core API namespace
using Aspose.Pdf.Facades;            // For stamp-related classes (if needed)

class AddImageWatermark
{
    static void Main()
    {
        // Input PDF and watermark image paths
        const string inputPdfPath   = "input.pdf";
        const string watermarkPath  = "watermark.png";
        const string outputPdfPath  = "watermarked.pdf";

        // Verify files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(watermarkPath))
        {
            Console.Error.WriteLine($"Watermark image not found: {watermarkPath}");
            return;
        }

        // Load the PDF document (using statement ensures deterministic disposal)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create an ImageStamp from the image file
            ImageStamp imgStamp = new ImageStamp(watermarkPath);

            // Ensure the stamp is fully opaque (default is 1.0, set explicitly for clarity)
            imgStamp.Opacity = 1.0f;

            // Optional: place the stamp at the center of each page
            imgStamp.HorizontalAlignment = HorizontalAlignment.Center;
            imgStamp.VerticalAlignment   = VerticalAlignment.Center;

            // Add the stamp to every page in the document
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