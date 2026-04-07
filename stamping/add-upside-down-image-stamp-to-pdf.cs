using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string stampImg  = "stamp.png"; // path to the image to be used as stamp

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(stampImg))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImg}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Choose the page to stamp (first page in this example)
            Page page = doc.Pages[1];

            // Create an image stamp from the image file
            ImageStamp imgStamp = new ImageStamp(stampImg);

            // Position the stamp on the page (coordinates are from the bottom‑left corner)
            imgStamp.XIndent = 100; // horizontal offset from the left edge
            imgStamp.YIndent = 150; // vertical offset from the bottom edge

            // Rotate the stamp 180 degrees (upside‑down)
            imgStamp.Rotate = Rotation.on180; // correct enum member (prefix "on")

            // Add the stamp to the page
            page.AddStamp(imgStamp);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Image stamp added and saved to '{outputPdf}'.");
    }
}
