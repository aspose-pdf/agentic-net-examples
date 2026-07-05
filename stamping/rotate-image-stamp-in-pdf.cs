using System;
using System.IO;
using Aspose.Pdf;               // Core API (Document, Page, ImageStamp, etc.)

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";      // source PDF
        const string outputPdf = "output.pdf";     // result PDF
        const string stampImg  = "logo.png";       // image to use as stamp

        // Verify files exist
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

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document doc = new Document(inputPdf))
        {
            // Choose the page where the stamp will be placed (first page in this example)
            Page page = doc.Pages[1];   // 1‑based indexing (global rule)

            // Create an ImageStamp from the image file
            ImageStamp imgStamp = new ImageStamp(stampImg);

            // Set desired position and size (optional)
            imgStamp.XIndent = 100;      // distance from left edge
            imgStamp.YIndent = 500;      // distance from bottom edge
            imgStamp.Width   = 150;      // stamp width
            imgStamp.Height  = 100;      // stamp height

            // Rotate the stamp by an arbitrary angle (45 degrees)
            imgStamp.RotateAngle = 45;   // property defined on Stamp base class

            // Add the stamp to the page (Page.AddStamp is the correct method)
            page.AddStamp(imgStamp);

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Stamp applied and saved to '{outputPdf}'.");
    }
}