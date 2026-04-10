using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "watermarked.pdf";
        const string imagePath  = "watermark.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Watermark image not found: {imagePath}");
            return;
        }

        // Load the PDF document (lifecycle: load)
        using (Document doc = new Document(inputPath))
        {
            // Create an ImageStamp for the watermark image
            // Fully qualify to avoid ambiguity with other Stamp types
            ImageStamp stamp = new ImageStamp(imagePath);

            // Scale to half of the page size (Zoom = 0.5)
            stamp.Zoom = 0.5;

            // Rotate the image by 45 degrees
            stamp.RotateAngle = 45;

            // Position the stamp at the center of each page
            // HorizontalAlignment and VerticalAlignment center the stamp automatically
            stamp.HorizontalAlignment = HorizontalAlignment.Center;
            stamp.VerticalAlignment   = VerticalAlignment.Center;

            // Apply the stamp to every page in the document
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(stamp);
            }

            // Save the modified PDF (lifecycle: save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}