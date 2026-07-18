using System;
using System.IO;
using Aspose.Pdf;

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

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Create an ImageStamp from the image file
            ImageStamp imgStamp = new ImageStamp(stampImg);

            // Rotate the stamp 180 degrees (upside‑down)
            // The Rotation enum uses the 'on' prefix for multiples of 90°.
            imgStamp.Rotate = Rotation.on180;   // equivalent to 180°
            // Alternatively: imgStamp.RotateAngle = 180;

            // Optional: set position and size of the stamp
            imgStamp.XIndent = 100;   // distance from the left edge of the page
            imgStamp.YIndent = 100;   // distance from the bottom edge of the page
            imgStamp.Width   = 150;   // stamp width
            imgStamp.Height  = 150;   // stamp height

            // Add the stamp to the first page (pages are 1‑based)
            Page firstPage = doc.Pages[1];
            firstPage.AddStamp(imgStamp);

            // Save the modified PDF (lifecycle rule: save inside using block)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Image stamp added and saved to '{outputPdf}'.");
    }
}