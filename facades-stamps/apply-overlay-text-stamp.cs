using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string imagePath = "logo.png"; // image used for the stamp

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine("Aspose.Pdf.Facades.Stamp image not found: " + imagePath);
            return;
        }

        // Create a stamp and configure it to overlay existing content
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
        stamp.BindImage(imagePath);
        stamp.IsBackground = false; // ensure the stamp is placed on top of page content
        stamp.SetOrigin(100f, 500f);
        stamp.SetImageSize(200f, 100f);
        stamp.Opacity = 0.7f;

        // Apply the stamp to all pages of the PDF using PdfFileStamp
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPath);
        fileStamp.AddStamp(stamp);
        fileStamp.Save(outputPath);
        fileStamp.Close();

        Console.WriteLine("Aspose.Pdf.Facades.Stamp applied and saved to " + outputPath);
    }
}