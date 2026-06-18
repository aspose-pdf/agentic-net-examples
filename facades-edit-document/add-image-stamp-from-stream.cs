using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string imagePath     = "logo.png";

        // Verify that source files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Initialize the facade and bind the source PDF
        PdfFileStamp pdfFileStamp = new PdfFileStamp();
        pdfFileStamp.BindPdf(inputPdfPath);

        // Create a stamp object
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();

        // Bind the image from a stream (no intermediate file is created)
        using (FileStream imageStream = File.OpenRead(imagePath))
        {
            stamp.BindImage(imageStream);
        }

        // Configure stamp appearance
        stamp.SetOrigin(100, 500);          // Position (X, Y) from bottom‑left corner
        stamp.SetImageSize(150, 100);       // Width and height of the stamp
        stamp.Opacity = 0.6f;               // Semi‑transparent
        stamp.IsBackground = false;         // Place on top of page content

        // Add the stamp to the document (null Pages means all pages)
        pdfFileStamp.AddStamp(stamp);

        // Save the stamped PDF
        pdfFileStamp.Save(outputPdfPath);
        pdfFileStamp.Close();

        Console.WriteLine($"Image stamp applied and saved to '{outputPdfPath}'.");
    }
}