using System;
using System.IO;
using Aspose.Pdf.Facades; // Facade API for stamping

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // source PDF
        const string outputPdf = "output.pdf";  // result PDF
        const string imagePath = "stamp.png";   // image to use as stamp

        // Validate files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Stamp image not found: {imagePath}");
            return;
        }

        // Create a stamp and configure its properties
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
        stamp.BindImage(imagePath);          // use the image as stamp content
        stamp.SetOrigin(100, 200);           // place stamp at (100,200) – lower‑left origin
        stamp.Rotation = 45f;                // rotate 45 degrees (arbitrary angle)
        stamp.Opacity = 0.8f;                // 80 % opacity
        stamp.IsBackground = false;          // stamp appears on top of page content

        // Initialize the facade, bind the source PDF, add the stamp and save
        Aspose.Pdf.Facades.PdfFileStamp pdfFileStamp = new Aspose.Pdf.Facades.PdfFileStamp();
        pdfFileStamp.BindPdf(inputPdf);       // load the source PDF
        pdfFileStamp.AddStamp(stamp);         // add the configured stamp
        pdfFileStamp.Save(outputPdf);         // write the result
        pdfFileStamp.Close();                 // release resources

        Console.WriteLine($"Image stamp added and saved to '{outputPdf}'.");
    }
}