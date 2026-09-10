using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";   // source PDF
        const string outputPdf = "output.pdf"; // result PDF
        const string stampImg = "stamp.png";   // image to use as stamp

        if (!File.Exists(inputPdf) || !File.Exists(stampImg))
        {
            Console.Error.WriteLine("Input PDF or stamp image not found.");
            return;
        }

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document doc = new Document(inputPdf))
        {
            // Create an image stamp from a file (ImageStamp(string) constructor)
            ImageStamp imgStamp = new ImageStamp(stampImg);

            // Rotate the stamp 180 degrees (use the correct enum values with 'on' prefix)
            imgStamp.Rotate = Rotation.on180;

            // Position the stamp on the page (coordinates are from the lower‑left corner)
            imgStamp.XIndent = 100; // distance from the left edge
            imgStamp.YIndent = 100; // distance from the bottom edge

            // Optionally set size or opacity here, e.g.:
            // imgStamp.Width  = 200;
            // imgStamp.Height = 100;
            // imgStamp.Opacity = 0.8;

            // Add the stamp to the first page (Page.AddStamp method)
            doc.Pages[1].AddStamp(imgStamp);

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Image stamp added and saved to '{outputPdf}'.");
    }
}