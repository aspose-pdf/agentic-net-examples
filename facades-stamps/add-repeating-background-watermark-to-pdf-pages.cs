using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF, output PDF and watermark image paths
        const string inputPdf   = "input.pdf";
        const string outputPdf  = "output.pdf";
        const string watermark  = "watermark.png";

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

        // Initialize the PdfFileStamp facade and bind the source PDF
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPdf);               // Load the document to be stamped

        // Create a single Stamp instance that will be reused for all pages
        Stamp stamp = new Stamp();

        // Bind the watermark image to the stamp
        stamp.BindImage(watermark);

        // Configure the stamp as a background watermark
        stamp.IsBackground = true;                 // Place behind page content
        stamp.Opacity      = 0.5f;                 // Semi‑transparent

        // Position and size of the watermark on each page
        stamp.SetOrigin(0, 0);                     // Bottom‑left corner
        stamp.SetImageSize(100, 100);              // Width and height in points

        // By default, Pages == null, so the stamp is applied to every page.
        // No need to set stamp.Pages explicitly.

        // Add the stamp to the document
        fileStamp.AddStamp(stamp);

        // Save the stamped PDF and release resources
        fileStamp.Save(outputPdf);
        fileStamp.Close();

        Console.WriteLine($"Watermarked PDF saved to '{outputPdf}'.");
    }
}