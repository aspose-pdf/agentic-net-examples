using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string stampImagePath = "stamp.png";
        const string outputPdfPath  = "output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(stampImagePath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImagePath}");
            return;
        }

        // Load the existing PDF (preserves all annotations)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create an image stamp
            ImageStamp imgStamp = new ImageStamp(stampImagePath)
            {
                // Position the stamp – centered horizontally, at the bottom of the page
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Bottom,
                // Make the stamp semi‑transparent
                Opacity = 0.5f
            };

            // Add the stamp to the first page (page indexing is 1‑based)
            pdfDoc.Pages[1].AddStamp(imgStamp);

            // Save the PDF; existing annotations remain intact
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Image stamp added and saved to '{outputPdfPath}'.");
    }
}