using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string stampImagePath = "stamp.png";

        if (!File.Exists(inputPdfPath) || !File.Exists(stampImagePath))
        {
            Console.Error.WriteLine("Input PDF or stamp image not found.");
            return;
        }

        // Load the PDF document (lifecycle rule: using Document)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create an image stamp from the image file
            ImageStamp imgStamp = new ImageStamp(stampImagePath);

            // Rotate the stamp by 45 degrees (arbitrary angle)
            imgStamp.RotateAngle = 45;

            // Optional: position the stamp (centered on the page)
            imgStamp.HorizontalAlignment = HorizontalAlignment.Center;
            imgStamp.VerticalAlignment   = VerticalAlignment.Center;

            // Add the rotated stamp to the first page
            pdfDoc.Pages[1].AddStamp(imgStamp);

            // Save the modified PDF (lifecycle rule: using Document)
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Rotated image stamp added and saved to '{outputPdfPath}'.");
    }
}