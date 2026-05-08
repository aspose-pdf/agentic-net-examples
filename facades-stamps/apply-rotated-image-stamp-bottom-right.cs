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
        const string imagePath  = "stamp.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Aspose.Pdf.Facades.Stamp image not found: {imagePath}");
            return;
        }

        // Initialize the facade with input and output PDF files
        PdfFileStamp fileStamp = new PdfFileStamp(inputPath, outputPath);

        // Create a stamp and bind the image to it
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
        stamp.BindImage(imagePath);

        // Set the desired size of the stamp (width, height)
        float stampWidth  = 100f;
        float stampHeight = 100f;
        stamp.SetImageSize(stampWidth, stampHeight);

        // Rotate the stamp by 30 degrees
        stamp.Rotation = 30f;

        // Position the stamp at the bottom‑right corner of each page
        // Use a small margin from the page edges
        float margin   = 10f;
        float originX  = fileStamp.PageWidth  - stampWidth  - margin;
        float originY  = margin; // bottom margin
        stamp.SetOrigin(originX, originY);

        // Apply the stamp to all pages of the document
        fileStamp.AddStamp(stamp);

        // Save and close the result
        fileStamp.Close();

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}