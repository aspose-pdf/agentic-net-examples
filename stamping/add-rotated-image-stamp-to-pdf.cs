using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string stampImage = "stamp.png";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(stampImage))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImage}");
            return;
        }

        // Wrap Document in a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Create an image stamp from the PNG/JPG file
            ImageStamp imgStamp = new ImageStamp(stampImage);

            // Rotate the stamp 45 degrees clockwise
            imgStamp.RotateAngle = 45;

            // Optional: set alignment or position on the page
            imgStamp.HorizontalAlignment = HorizontalAlignment.Left;
            imgStamp.VerticalAlignment   = VerticalAlignment.Top;

            // Pages are 1‑based; add the stamp to the first page
            doc.Pages[1].AddStamp(imgStamp);

            // Save the modified PDF (default Save writes PDF)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Stamp added and rotated 45°; saved to '{outputPdf}'.");
    }
}