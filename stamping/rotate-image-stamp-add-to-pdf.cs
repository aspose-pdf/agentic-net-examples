using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string imagePath = "stamp.png";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf) || !File.Exists(imagePath))
        {
            Console.Error.WriteLine("Required files not found.");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Create an image stamp from the specified image file
            ImageStamp imgStamp = new ImageStamp(imagePath);

            // Rotate the stamp by 45 degrees
            imgStamp.RotateAngle = 45;

            // Position the stamp (optional: center it on the page)
            imgStamp.HorizontalAlignment = HorizontalAlignment.Center;
            imgStamp.VerticalAlignment   = VerticalAlignment.Center;

            // Add the rotated stamp to the first page
            doc.Pages[1].AddStamp(imgStamp);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Rotated image stamp saved to '{outputPdf}'.");
    }
}