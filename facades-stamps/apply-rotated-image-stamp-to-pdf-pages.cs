using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";      // source PDF
        const string outputPdf = "output.pdf";     // destination PDF
        const string imagePath = "stamp.png";      // image to use as stamp

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Initialize the facade and bind the source PDF
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPdf);

        // Create a stamp and bind the image
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
        stamp.BindImage(imagePath);

        // Define stamp size (width x height in points)
        const float stampWidth  = 100f;
        const float stampHeight = 100f;
        stamp.SetImageSize(stampWidth, stampHeight);

        // Position the stamp at the bottom‑right corner of each page
        // PageWidth / PageHeight refer to the first page; they are the same for all pages in most PDFs
        float xPos = fileStamp.PageWidth  - stampWidth; // distance from left edge
        float yPos = 0f;                               // distance from bottom edge
        stamp.SetOrigin(xPos, yPos);

        // Apply a 30‑degree rotation (arbitrary angle)
        stamp.Rotation = 30f;   // Rotation property accepts any degree value

        // Add the stamp to the document (applies to all pages by default)
        fileStamp.AddStamp(stamp);

        // Save the result
        fileStamp.Save(outputPdf);
        fileStamp.Close();

        Console.WriteLine($"Rotated image stamp applied and saved to '{outputPdf}'.");
    }
}