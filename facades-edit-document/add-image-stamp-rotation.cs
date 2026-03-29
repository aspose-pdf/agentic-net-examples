using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string imagePath = "stamp.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        PdfFileStamp fileStamp = new PdfFileStamp(inputPath, outputPath);
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
        stamp.BindImage(imagePath);
        stamp.SetOrigin(100f, 200f);
        stamp.Rotation = 45f;
        stamp.Opacity = 0.8f;
        fileStamp.AddStamp(stamp);
        fileStamp.Close();

        Console.WriteLine($"Image stamp added and saved to '{outputPath}'.");
    }
}