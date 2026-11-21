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

        // Load the PDF inside a using block for deterministic disposal.
        using (Document pdfDocument = new Document(inputPath))
        {
            // Create an ImageStamp, rotate it 90 degrees, and center it on each page.
            ImageStamp imgStamp = new ImageStamp(stampPath)
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center,
                RotateAngle         = 90,    // 90° rotation to match landscape orientation
                Opacity             = 0.5,   // optional semi‑transparent appearance
                Background          = false  // draw on top of existing content
            };

            // Apply the stamp to each page individually (Page.AddStamp, not PageCollection).
            foreach (Page page in pdfDocument.Pages)
            {
                page.AddStamp(imgStamp);
            }

            // Save the modified PDF.
            pdfDocument.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}