using System;
using System.IO;
using Aspose.Pdf.Facades;   // Facade API for stamping
using Aspose.Pdf;          // Core types (Aspose.Pdf.Facades.Stamp)

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // source PDF
        const string outputPdf = "output.pdf";  // stamped PDF
        const string imagePath = "stamp.png";   // image to use as stamp

        // Verify files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Aspose.Pdf.Facades.Stamp image not found: {imagePath}");
            return;
        }

        // Initialize the PdfFileStamp facade
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.InputFile  = inputPdf;
        fileStamp.OutputFile = outputPdf;

        // Create and configure the stamp
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();                 // default constructor
        stamp.BindImage(imagePath);                // use the image file
        stamp.SetOrigin(100, 500);                 // position on the page (X, Y)
        stamp.SetImageSize(150, 100);              // width, height of the stamp
        stamp.Opacity = 0.8f;                      // semi‑transparent
        stamp.IsBackground = false;                // place on top of page content
        stamp.PageNumber = 2;                      // apply only to the second page

        // Add the stamp to the PDF and finalize
        fileStamp.AddStamp(stamp);
        fileStamp.Close(); // saves the output file
    }
}