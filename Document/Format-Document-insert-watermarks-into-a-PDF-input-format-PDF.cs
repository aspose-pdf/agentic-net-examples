using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf      = "input.pdf";
        const string outputPdf     = "watermarked.pdf";
        const string watermarkPath = "watermark.png";

        // Verify that required files exist
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

        // Configure the PdfFileStamp facade with input and output files
        PdfFileStamp pdfStamp = new PdfFileStamp
        {
            InputFile  = inputPdf,
            OutputFile = outputPdf
        };

        // Create a stamp, bind the image, and set its appearance
        Stamp stamp = new Stamp();
        stamp.BindImage(watermarkPath);          // use image file directly
        stamp.SetOrigin(100, 400);               // lower‑left corner (adjust as needed)
        stamp.SetImageSize(200, 100);            // width & height of the watermark
        stamp.IsBackground = true;               // place behind existing content
        stamp.Opacity = 0.5f;                    // semi‑transparent

        // Apply the stamp to all pages of the document
        pdfStamp.AddStamp(stamp);
        pdfStamp.Close(); // finalize and write the output file

        Console.WriteLine($"Watermarked PDF saved to '{outputPdf}'.");
    }
}