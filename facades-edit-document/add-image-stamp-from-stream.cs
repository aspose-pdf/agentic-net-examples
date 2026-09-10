using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";   // source PDF
        const string outputPdfPath = "output.pdf";  // stamped PDF
        const string imagePath     = "stamp.png";   // image to use as stamp

        // Ensure source files exist
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

        // Open the image as a stream – no intermediate file is created
        using (FileStream imageStream = File.OpenRead(imagePath))
        {
            // Create a stamp and bind the image stream to it
            Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
            stamp.BindImage(imageStream);               // use the stream directly
            stamp.SetOrigin(100, 500);                  // position (X, Y) from lower‑left corner
            stamp.SetImageSize(200, 100);               // width, height in points
            stamp.Opacity = 0.6f;                       // semi‑transparent
            stamp.IsBackground = true;                  // place behind page content

            // Use PdfFileStamp facade to apply the stamp to the PDF
            using (PdfFileStamp pdfFileStamp = new PdfFileStamp())
            {
                pdfFileStamp.BindPdf(inputPdfPath);      // load source PDF
                pdfFileStamp.AddStamp(stamp);            // add the prepared stamp
                pdfFileStamp.Save(outputPdfPath);        // write result
            }
        }

        Console.WriteLine($"Image stamp applied and saved to '{outputPdfPath}'.");
    }
}