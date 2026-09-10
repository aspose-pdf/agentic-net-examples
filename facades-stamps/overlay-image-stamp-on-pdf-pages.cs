using System;
using System.IO;
using Aspose.Pdf.Facades; // Facade API for stamping

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // source PDF
        const string outputPath = "output.pdf";  // stamped PDF
        const string imagePath  = "stamp.png";   // image to use as stamp

        // Verify required files exist
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Stamp image not found: {imagePath}");
            return;
        }

        // Initialize PdfFileStamp with input and output file paths
        PdfFileStamp fileStamp = new PdfFileStamp(inputPath, outputPath);

        // Create a Stamp object from the Facades namespace
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();

        // Bind an image to the stamp (you could also use BindLogo for text)
        stamp.BindImage(imagePath);

        // Explicitly set IsBackground to false so the stamp overlays existing content
        stamp.IsBackground = false;

        // Optional: configure position, size, and opacity of the stamp
        stamp.SetOrigin(100, 200);          // X and Y coordinates
        stamp.SetImageSize(150, 100);       // Width and height
        stamp.Opacity = 0.8f;               // 80% opacity

        // Add the stamp to the document (Pages = null means all pages)
        fileStamp.AddStamp(stamp);

        // Finalize and write the output PDF
        fileStamp.Close();

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}