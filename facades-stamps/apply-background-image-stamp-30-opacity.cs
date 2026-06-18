using System;
using System.IO;
using Aspose.Pdf.Facades;   // Facade classes for stamping

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // source PDF
        const string outputPdf = "output.pdf";  // destination PDF
        const string stampImg  = "stamp.png";   // image to use as background stamp

        // Validate required files
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(stampImg))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImg}");
            return;
        }

        // PdfFileStamp is a disposable facade – use a using block for deterministic cleanup
        using (PdfFileStamp fileStamp = new PdfFileStamp())
        {
            // Load the source PDF
            fileStamp.BindPdf(inputPdf);

            // Create a stamp instance (fully qualified to avoid ambiguity)
            Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();

            // Bind an image as the stamp content
            stamp.BindImage(stampImg);

            // Place the stamp behind page content
            stamp.IsBackground = true;

            // Set opacity to 30% (0.3f)
            stamp.Opacity = 0.3f;

            // Apply the stamp to all pages of the document
            fileStamp.AddStamp(stamp);

            // Save the stamped PDF
            fileStamp.Save(outputPdf);
        }

        Console.WriteLine($"Background stamp applied with 30% opacity: {outputPdf}");
    }
}