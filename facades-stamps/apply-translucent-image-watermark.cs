using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string imagePath  = "watermark.png"; // image to use as watermark

        // Verify required files exist
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Watermark image not found: {imagePath}");
            return;
        }

        // Initialize PdfFileStamp facade
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.InputFile  = inputPath;
        fileStamp.OutputFile = outputPath;

        // Create a stamp, bind the image, set opacity to 50% and make it a background watermark
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
        stamp.BindImage(imagePath);   // use the image as the stamp content
        stamp.Opacity = 0.5f;         // 50% opacity for translucency
        stamp.IsBackground = true;   // place stamp behind page content (watermark effect)

        // Apply the stamp to all pages (Pages = null by default)
        fileStamp.AddStamp(stamp);

        // Persist changes and release resources
        fileStamp.Close();

        Console.WriteLine($"Translucent watermark applied. Output saved to '{outputPath}'.");
    }
}