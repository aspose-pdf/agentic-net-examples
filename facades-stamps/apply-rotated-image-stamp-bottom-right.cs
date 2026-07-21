using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string imagePath = "stamp.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Aspose.Pdf.Facades.Stamp image not found: {imagePath}");
            return;
        }

        // Initialize the facade with input and output files
        PdfFileStamp fileStamp = new PdfFileStamp(inputPdf, outputPdf);

        // Create a stamp and bind the image
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
        stamp.BindImage(imagePath);

        // Define stamp size (width, height) in points
        const float stampWidth  = 100f;
        const float stampHeight = 100f;
        stamp.SetImageSize(stampWidth, stampHeight);

        // Rotate the stamp by an arbitrary angle (30 degrees)
        stamp.Rotation = 30f; // Facades Aspose.Pdf.Facades.Stamp uses a float for rotation

        // Position the stamp at the bottom‑right corner of each page
        // Page dimensions are available after binding the PDF
        float pageWidth  = fileStamp.PageWidth;
        // Y coordinate is 0 (bottom); X is page width minus stamp width
        stamp.SetOrigin(pageWidth - stampWidth, 0f);

        // Apply the stamp to all pages (null means all pages)
        stamp.Pages = null;

        // Add the configured stamp to the document
        fileStamp.AddStamp(stamp);

        // Save and close the facade
        fileStamp.Close();

        Console.WriteLine($"Rotated image stamp applied and saved to '{outputPdf}'.");
    }
}