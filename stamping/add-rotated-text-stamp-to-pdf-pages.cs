using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string stampPath  = "stamp.png";
        const string outputPath = "stamped_output.pdf";

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
        using (Document pdfDocument = new Document(inputPath))
        {
            // Create an image stamp and rotate it 90 degrees
            ImageStamp imgStamp = new ImageStamp(stampPath)
            {
                RotateAngle        = 90,                     // rotate to match portrait‑to‑landscape
                Background         = false,                  // place stamp over content
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center
            };

            // Apply the stamp to each page individually
            foreach (Page page in pdfDocument.Pages)
            {
                page.AddStamp(imgStamp);
            }

            // Save the modified PDF
            pdfDocument.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}