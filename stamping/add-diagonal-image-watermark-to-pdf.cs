using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string stampImagePath = "watermark.png";
        const string outputPath = "watermarked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(stampImagePath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImagePath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPath))
        {
            // Create an ImageStamp from the watermark image
            ImageStamp imgStamp = new ImageStamp(stampImagePath)
            {
                Background = false,                     // place on top of page content
                Opacity = 0.3,                          // semi‑transparent (optional)
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                // Rotate the stamp 90 degrees to obtain a diagonal watermark
                Rotate = Rotation.on90
            };

            // Apply the stamp to each page individually
            foreach (Page page in pdfDocument.Pages)
            {
                page.AddStamp(imgStamp);
            }

            // Save the modified PDF while the document is still alive
            pdfDocument.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}