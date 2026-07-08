using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // source PDF
        const string outputPdf = "output.pdf";  // stamped PDF
        const string imagePath = "stampImage.png"; // image to use as stamp

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Initialize the facade and bind the source PDF
        using (PdfFileStamp fileStamp = new PdfFileStamp())
        {
            fileStamp.BindPdf(inputPdf); // load source document

            // Create a stamp, bind the image, set properties
            Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
            stamp.BindImage(imagePath);          // set image for the stamp
            stamp.Opacity = 0.8f;                // 80% opacity
            stamp.Rotation = 45f;                // rotate 45 degrees
            stamp.SetOrigin(100f, 200f);         // place at (100,200)

            // Add the stamp to all pages (Pages = null means all pages)
            fileStamp.AddStamp(stamp);

            // Save the result
            fileStamp.Save(outputPdf);
            fileStamp.Close();
        }

        Console.WriteLine($"Image stamp applied and saved to '{outputPdf}'.");
    }
}