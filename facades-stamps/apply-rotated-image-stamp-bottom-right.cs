using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string imagePath = "stamp.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Stamp image not found: {imagePath}");
            return;
        }

        // Retrieve page dimensions (assumes all pages have the same size)
        PdfFileInfo info = new PdfFileInfo();
        info.BindPdf(inputPdf);
        double pageWidth = info.GetPageWidth(1);
        double pageHeight = info.GetPageHeight(1);
        info.Close();

        // Initialize the PdfFileStamp facade and bind the source PDF
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPdf);

        // Create and configure the image stamp
        Stamp stamp = new Stamp();
        stamp.BindImage(imagePath);
        // Optional: set the stamp size (width, height) in points
        stamp.SetImageSize(50f, 50f);
        // Rotate the stamp by 30 degrees
        stamp.Rotation = 30f;
        // Position the stamp at the bottom‑right corner of each page
        // SetOrigin defines the lower‑left corner of the stamp
        float x = (float)(pageWidth - 50); // page width minus stamp width
        float y = 0f;                       // bottom edge
        stamp.SetOrigin(x, y);

        // Apply the stamp to all pages of the document
        fileStamp.AddStamp(stamp);

        // Save the modified PDF and release resources
        fileStamp.Save(outputPdf);
        fileStamp.Close();

        Console.WriteLine($"Rotated image stamp applied. Output saved to '{outputPdf}'.");
    }
}