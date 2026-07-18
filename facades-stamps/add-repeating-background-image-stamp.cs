using System;
using System.IO;
using Aspose.Pdf.Facades;   // PdfFileStamp and Stamp are in this namespace

class Program
{
    static void Main()
    {
        // Paths for the source PDF, the watermark image, and the output PDF
        const string inputPdf   = "input.pdf";
        const string watermark  = "watermark.png";
        const string outputPdf  = "watermarked_output.pdf";

        // Verify that required files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(watermark))
        {
            Console.Error.WriteLine($"Watermark image not found: {watermark}");
            return;
        }

        // Create the PdfFileStamp facade
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.InputFile  = inputPdf;   // source document
        fileStamp.OutputFile = outputPdf;  // destination document

        // Create a single Stamp instance that will be applied to all pages
        Stamp stamp = new Stamp();

        // Bind the image that will be used as the watermark
        stamp.BindImage(watermark);

        // Make the stamp a background element so it appears behind page content
        stamp.IsBackground = true;

        // Optional: set opacity (0.0 = fully transparent, 1.0 = fully opaque)
        stamp.Opacity = 0.5f;

        // By default, Pages == null, meaning the stamp is applied to every page.
        // If you need to limit pages, assign an int[] array, e.g. stamp.Pages = new int[] {1,3,5};

        // Add the stamp to the file
        fileStamp.AddStamp(stamp);

        // Persist changes and release resources
        fileStamp.Close();

        Console.WriteLine($"Watermarked PDF saved to '{outputPdf}'.");
    }
}