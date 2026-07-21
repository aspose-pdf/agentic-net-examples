using System;
using System.IO;
using Aspose.Pdf.Facades;   // PdfFileStamp and Stamp are defined here

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string imagePath = "stamp.png";

        // Verify that the source PDF and image exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Initialize the facade and bind the source PDF
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPdf);

        // Create a stamp, bind the image, and configure its properties
        Stamp stamp = new Stamp();
        stamp.BindImage(imagePath);          // use the image as stamp content
        stamp.SetOrigin(100, 200);           // position (lower‑left corner) at (100,200)
        // RotationAngle property does not exist in this version of Aspose.Pdf.Facades.Stamp.
        // Use the Rotation property (or SetRotationAngle method in newer versions) instead.
        stamp.Rotation = 45f;                // rotate 45 degrees
        stamp.Opacity = 0.8f;                // 80 % opacity

        // Add the stamp to the document (applies to all pages by default)
        fileStamp.AddStamp(stamp);

        // Save the resulting PDF and release resources
        fileStamp.Save(outputPdf);
        fileStamp.Close();

        Console.WriteLine($"Image stamp added and saved to '{outputPdf}'.");
    }
}
