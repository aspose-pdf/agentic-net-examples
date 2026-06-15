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
        const string stampImage = "logo.png"; // image to use as stamp

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        if (!File.Exists(stampImage))
        {
            Console.Error.WriteLine($"Aspose.Pdf.Facades.Stamp image not found: {stampImage}");
            return;
        }

        // Initialize the facade and bind the source PDF
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPdf);               // load source PDF

        // Create a stamp that will be applied to all pages (Pages = null by default)
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();

        // Configure the stamp – here we use an image stamp
        stamp.BindImage(stampImage);               // set image source
        stamp.SetOrigin(100, 500);                 // position (X, Y) from bottom‑left
        stamp.SetImageSize(120, 80);               // width and height
        stamp.IsBackground = true;                 // place behind page content
        stamp.Opacity = 0.6f;                      // semi‑transparent

        // Add the stamp to the document – it will affect every page
        fileStamp.AddStamp(stamp);

        // Save the result and release resources
        fileStamp.Save(outputPdf);
        fileStamp.Close();

        Console.WriteLine($"Aspose.Pdf.Facades.Stamp applied to all pages. Output saved to '{outputPdf}'.");
    }
}