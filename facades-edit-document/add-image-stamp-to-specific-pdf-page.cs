using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // source PDF
        const string outputPdf = "output.pdf";  // result PDF
        const string imagePath = "stampImage.png"; // image to use as stamp

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

        // Initialize the facade and specify input/output files
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.InputFile  = inputPdf;
        fileStamp.OutputFile = outputPdf;

        // Create a stamp, bind the image, and configure its appearance
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
        stamp.BindImage(imagePath);               // use the image file
        stamp.SetOrigin(140, 400);                // position on the page (X, Y)
        stamp.SetImageSize(50, 50);               // width and height of the stamp
        stamp.Opacity = 0.8f;                     // semi‑transparent
        stamp.IsBackground = true;                // place behind page content
        stamp.Pages = new int[] { 2 };            // apply only to page 2 (1‑based)

        // Add the configured stamp to the PDF
        fileStamp.AddStamp(stamp);

        // Finalize and write the output file
        fileStamp.Close();

        Console.WriteLine($"Aspose.Pdf.Facades.Stamp added to page 2 and saved as '{outputPdf}'.");
    }
}