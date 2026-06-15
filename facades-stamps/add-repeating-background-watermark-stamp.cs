using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf      = "input.pdf";
        const string outputPdf     = "output.pdf";
        const string watermarkPath = "watermark.png";

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

        // Initialize the PdfFileStamp facade and specify input/output files
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.InputFile  = inputPdf;
        fileStamp.OutputFile = outputPdf;

        // Create a single stamp instance (fully qualified to avoid ambiguity)
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();

        // Bind the image that will be used as the watermark
        stamp.BindImage(watermarkPath);

        // Make the stamp a background element so it appears behind page content
        stamp.IsBackground = true;

        // Optional visual settings
        stamp.Opacity = 0.3f;                 // semi‑transparent
        stamp.SetImageSize(200, 200);         // width and height in points

        // Apply the same stamp to all pages (Pages = null by default)
        fileStamp.AddStamp(stamp);

        // Persist the changes
        fileStamp.Close();

        Console.WriteLine($"Watermarked PDF saved to '{outputPdf}'.");
    }
}