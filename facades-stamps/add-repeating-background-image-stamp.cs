using System;
using System.IO;
using Aspose.Pdf.Facades; // Facade API for stamping

class Program
{
    static void Main()
    {
        const string inputPdf      = "input.pdf";      // source PDF
        const string outputPdf     = "output.pdf";     // destination PDF
        const string watermarkPath = "watermark.png";  // image to use as watermark

        // Validate files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(watermarkPath))
        {
            Console.Error.WriteLine($"Watermark image not found: {watermarkPath}");
            return;
        }

        // ------------------------------------------------------------
        // Create the PdfFileStamp facade and point it to the source/target
        // ------------------------------------------------------------
        Aspose.Pdf.Facades.PdfFileStamp fileStamp = new Aspose.Pdf.Facades.PdfFileStamp();
        fileStamp.InputFile  = inputPdf;
        fileStamp.OutputFile = outputPdf;

        // ------------------------------------------------------------
        // Create a single Stamp instance that will be reused for all pages
        // ------------------------------------------------------------
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();

        // Bind the image that will act as the watermark
        stamp.BindImage(watermarkPath);

        // Place the stamp behind page content (background)
        stamp.IsBackground = true;

        // Make the watermark semi‑transparent
        stamp.Opacity = 0.5f;

        // Optional: adjust size and position if needed
        // stamp.SetImageSize(120, 80);   // width, height in points
        // stamp.SetOrigin(0, 0);        // lower‑left corner of the page

        // ------------------------------------------------------------
        // Apply the stamp to every page (Pages = null means all pages)
        // ------------------------------------------------------------
        fileStamp.AddStamp(stamp);

        // Persist the changes and release resources
        fileStamp.Close();

        Console.WriteLine($"Watermarked PDF saved to '{outputPdf}'.");
    }
}