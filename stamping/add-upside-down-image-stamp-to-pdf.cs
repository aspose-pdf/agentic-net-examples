using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";      // source PDF
        const string outputPdf = "output.pdf";    // result PDF
        const string stampImg = "logo.png";      // image to use as stamp

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        if (!File.Exists(stampImg))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImg}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Create an ImageStamp from the image file and configure it
            ImageStamp imgStamp = new ImageStamp(stampImg)
            {
                XIndent = 100,          // horizontal coordinate (from left)
                YIndent = 150,          // vertical coordinate (from bottom)
                Zoom = 0.5,             // 50% of original size
                Rotate = Rotation.on180 // rotate 180° (upside‑down)
            };

            // Add the stamp to the first page (pages are 1‑based)
            doc.Pages[1].AddStamp(imgStamp);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Image stamp added and saved to '{outputPdf}'.");
    }
}
