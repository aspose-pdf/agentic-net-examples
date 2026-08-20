using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // source PDF
        const string stampImg  = "stamp.png";   // image to use as background stamp
        const string outputPdf = "output.pdf";  // result PDF

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        if (!File.Exists(stampImg))
        {
            Console.Error.WriteLine($"Aspose.Pdf.Facades.Stamp image not found: {stampImg}");
            return;
        }

        // Initialize the facade and bind the source PDF
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPdf);

        // Create a stamp, bind the image, set opacity and background flag
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
        stamp.BindImage(stampImg);      // use the image as stamp content
        stamp.Opacity = 0.3f;           // 30% opacity
        stamp.IsBackground = true;     // place stamp behind page content

        // Add the stamp to all pages and save the result
        fileStamp.AddStamp(stamp);
        fileStamp.Save(outputPdf);
        fileStamp.Close();

        Console.WriteLine($"Background stamp applied. Output saved to '{outputPdf}'.");
    }
}