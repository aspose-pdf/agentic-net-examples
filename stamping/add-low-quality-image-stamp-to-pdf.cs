using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string stampPath = "stamp.png";   // source image for the stamp
        const string outputPath = "output.pdf";

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
            // Create an ImageStamp and configure low‑quality JPEG compression (10%)
            ImageStamp imgStamp = new ImageStamp(stampPath)
            {
                // Place the stamp in the center of each page
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center,
                // Set compression quality to 10% (range 0‑100)
                Quality = 10,
                // Optional: make the stamp semi‑transparent to keep it unobtrusive
                Opacity = 0.5
            };

            // Apply the stamp to every page individually (PageCollection has no AddStamp method)
            foreach (Page page in pdfDocument.Pages)
            {
                page.AddStamp(imgStamp);
            }

            // Save the modified PDF; the using block ensures the document stays alive until Save completes
            pdfDocument.Save(outputPath);
        }

        Console.WriteLine($"Low‑quality image stamp applied and saved to '{outputPath}'.");
    }
}
