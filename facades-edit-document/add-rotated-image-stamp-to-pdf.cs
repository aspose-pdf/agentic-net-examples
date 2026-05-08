using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string imagePath = "stampImage.png";

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

        // Initialize the facade with input and output files
        PdfFileStamp fileStamp = new PdfFileStamp(inputPdf, outputPdf);

        // Create a stamp, bind the image, set rotation, opacity and position
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
        stamp.BindImage(imagePath);      // use the image as stamp content
        stamp.Rotation = 45f;            // rotate 45 degrees
        stamp.Opacity = 0.8f;            // 80% opacity
        stamp.SetOrigin(100f, 200f);     // lower‑left corner at (100,200)

        // Add the stamp to the document (applies to all pages by default)
        fileStamp.AddStamp(stamp);

        // Finalize and write the output PDF
        fileStamp.Close();

        Console.WriteLine($"Image stamp added and saved to '{outputPdf}'.");
    }
}