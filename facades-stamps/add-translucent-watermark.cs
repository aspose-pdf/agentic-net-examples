using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string watermarkImagePath = "watermark.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        if (!File.Exists(watermarkImagePath))
        {
            Console.Error.WriteLine($"Watermark image not found: {watermarkImagePath}");
            return;
        }

        // Initialize the facade and bind the source PDF
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPath);

        // Create a stamp, set 50% opacity, and bind the image
        Stamp stamp = new Stamp();
        stamp.Opacity = 0.5f;               // 50 percent opacity
        stamp.IsBackground = true;          // place behind page content
        stamp.BindImage(watermarkImagePath);
        stamp.SetOrigin(100f, 400f);        // position on the page
        stamp.SetImageSize(200f, 100f);     // size of the image stamp

        // Apply the stamp to all pages and save the result
        fileStamp.AddStamp(stamp);
        fileStamp.Save(outputPath);
        fileStamp.Close();

        Console.WriteLine($"Translucent watermark applied and saved to '{outputPath}'.");
    }
}
