using System;
using System.IO;
using Aspose.Pdf;               // Core PDF API
using Aspose.Pdf.Facades;      // For ImageStamp (inherits from Stamp)

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // source PDF
        const string watermarkPath = "watermark.png";   // image to use as watermark
        const string outputPath = "watermarked.pdf";    // result PDF

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        if (!File.Exists(watermarkPath))
        {
            Console.Error.WriteLine($"Watermark image not found: {watermarkPath}");
            return;
        }

        // Load the PDF document (create/load rule)
        using (Document doc = new Document(inputPath))
        {
            // Create an image stamp with the watermark image
            ImageStamp imgStamp = new ImageStamp(watermarkPath);

            // Set arbitrary rotation angle (45 degrees) for diagonal placement
            imgStamp.RotateAngle = 45;          // Stamp.RotateAngle property

            // Optional: make the watermark semi‑transparent and place it over content
            imgStamp.Background = false;       // false = overlay (on top of page content)
            imgStamp.Opacity    = 0.5;          // 0..1 range

            // Apply the stamp to every page in the document
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(imgStamp);        // Page.AddStamp method
            }

            // Save the modified PDF (save rule)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}