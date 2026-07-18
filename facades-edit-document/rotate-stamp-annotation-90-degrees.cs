using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class RotateStampExample
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";      // source PDF
        const string outputPdf = "output.pdf";     // result PDF
        const string stampImg  = "stamp.png";      // image to be used as stamp

        // Verify that required files exist
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

        // Initialize the facade for stamping
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPdf);               // load source PDF

        // Create a stamp, bind the image, and rotate it 90° clockwise
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
        stamp.BindImage(stampImg);                 // use the image as stamp content
        stamp.Rotation = 90f;                      // rotate 90 degrees clockwise

        // Optionally set position and size (example values)
        stamp.SetOrigin(100, 100);                 // lower‑left corner of the stamp
        stamp.SetImageSize(200, 200);              // width and height in points

        // Add the stamp to the PDF
        fileStamp.AddStamp(stamp);

        // Save the result and release resources
        fileStamp.Save(outputPdf);
        fileStamp.Close();

        Console.WriteLine($"Stamp applied with 90° rotation. Output saved to '{outputPdf}'.");
    }
}