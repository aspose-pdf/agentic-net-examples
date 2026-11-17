using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string stampPath  = "stamp.png";
        const string outputPath = "output.pdf";

        // Verify input files exist
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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Create an image stamp with required quality and opacity
            ImageStamp imgStamp = new ImageStamp(stampPath)
            {
                Quality            = 100,   // 100 % image quality
                Opacity            = 0.8f,  // 80 % opacity
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center
            };

            // Ensure the document has at least two pages (pages are 1‑based)
            if (pdfDoc.Pages.Count >= 2)
            {
                // Add the stamp to page two
                pdfDoc.Pages[2].AddStamp(imgStamp);
            }
            else
            {
                Console.Error.WriteLine("The PDF has fewer than two pages.");
            }

            // Save the modified document
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"Image stamp added to page 2 and saved as '{outputPath}'.");
    }
}
