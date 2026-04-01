using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string stampImagePath = "stamp.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }
        if (!File.Exists(stampImagePath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImagePath}");
            return;
        }

        // Initialize PdfFileStamp and bind the source PDF
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPath);

        // Create a background stamp with 30% opacity
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
        stamp.BindImage(stampImagePath);
        stamp.IsBackground = true;
        stamp.Opacity = 0.3f;
        // Position and size (optional, adjust as needed)
        stamp.SetOrigin(100f, 200f);
        stamp.SetImageSize(200f, 200f);

        // Apply the stamp to all pages
        fileStamp.AddStamp(stamp);
        fileStamp.Save(outputPath);
        fileStamp.Close();

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}