using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // source PDF
        const string stampPath  = "stamp.png";   // image to use as stamp
        const string outputPath = "output.pdf";  // result PDF

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        if (!File.Exists(stampPath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document pdfDoc = new Document(inputPath))
        {
            // Create an image stamp
            ImageStamp stamp = new ImageStamp(stampPath)
            {
                // Place stamp in the centre of each page
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center,

                // Make the stamp semi‑transparent
                Opacity = 0.5,

                // Rotate the stamp content by 90° to match portrait‑to‑landscape pages
                Rotate = Rotation.on90
            };

            // Apply the stamp to every page
            foreach (Page page in pdfDoc.Pages)
            {
                page.AddStamp(stamp);
            }

            // Save the modified PDF (lifecycle rule: use Save without extra options for PDF)
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}
