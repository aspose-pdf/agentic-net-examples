using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";      // source PDF with existing annotations
        const string stampPath  = "stamp.png";      // image to use as stamp
        const string outputPath = "output.pdf";     // result PDF

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }

        if (!File.Exists(stampPath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPath))
        {
            // Create the image stamp and configure its appearance
            ImageStamp imgStamp = new ImageStamp(stampPath)
            {
                // false = stamp appears on top of page content (preserves annotations underneath)
                Background = false,
                // Adjust opacity if needed (1.0 = fully opaque)
                Opacity = 0.8,
                // Position the stamp (centered on the page)
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center
            };

            // Apply the stamp to each page individually; this preserves existing annotations
            foreach (Page page in pdfDocument.Pages)
            {
                page.AddStamp(imgStamp);
            }

            // Save the modified PDF; the original annotations remain intact
            pdfDocument.Save(outputPath);
        }

        Console.WriteLine($"Image stamp added and saved to '{outputPath}'.");
    }
}