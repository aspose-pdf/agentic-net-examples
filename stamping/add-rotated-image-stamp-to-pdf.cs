using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string stampImagePath = "stamp.png";
        const string outputPdfPath = "output.pdf";

        // Verify input files exist
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

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create an image stamp from the specified image file
            ImageStamp imgStamp = new ImageStamp(stampImagePath);

            // Rotate the stamp by an arbitrary angle (45 degrees)
            imgStamp.RotateAngle = 45;

            // Optional: set the position of the stamp on the page
            imgStamp.XIndent = 100; // distance from the left edge
            imgStamp.YIndent = 100; // distance from the bottom edge

            // Add the rotated stamp to the first page of the PDF
            pdfDoc.Pages[1].AddStamp(imgStamp);

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with rotated image stamp at '{outputPdfPath}'.");
    }
}