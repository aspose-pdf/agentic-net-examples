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
        const string imagePath = "watermark.png";

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

        // Create a stamp that will be used on all pages
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
        stamp.BindImage(imagePath);
        stamp.SetImageSize(100f, 100f);
        stamp.Opacity = 0.3f;
        stamp.IsBackground = true;
        // Position the stamp (example coordinates)
        stamp.SetOrigin(200f, 400f);
        // Apply to every page (null means all pages)
        stamp.Pages = null;

        // Apply the stamp to the PDF using PdfFileStamp
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPath);
        fileStamp.AddStamp(stamp);
        fileStamp.Save(outputPath);
        fileStamp.Close();

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}