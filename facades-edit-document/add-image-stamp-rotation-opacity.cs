using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string imagePath = "stamp.png";

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

        // Initialize PdfFileStamp with source and destination files
        Aspose.Pdf.Facades.PdfFileStamp fileStamp = new Aspose.Pdf.Facades.PdfFileStamp(inputPdf, outputPdf);

        // Create and configure the image stamp
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
        stamp.BindImage(imagePath);
        stamp.Rotation = 45f;          // rotate 45 degrees
        stamp.Opacity = 0.8f;          // 80% opacity
        stamp.SetOrigin(100f, 200f);   // position (100,200) from bottom‑left

        // Apply the stamp to all pages
        fileStamp.AddStamp(stamp);
        fileStamp.Close();

        Console.WriteLine($"Image stamp added and saved to '{outputPdf}'.");
    }
}
