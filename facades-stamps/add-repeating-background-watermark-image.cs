using System;
using System.IO;
using Aspose.Pdf;                     // Core PDF classes
using Aspose.Pdf.Facades;            // Facades API (PdfFileStamp, Stamp)

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string watermarkImage = "watermark.png";

        // Verify files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(watermarkImage))
        {
            Console.Error.WriteLine($"Watermark image not found: {watermarkImage}");
            return;
        }

        // ------------------------------------------------------------
        // 1. Initialise PdfFileStamp facade
        // ------------------------------------------------------------
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.InputFile  = inputPdf;   // source PDF
        fileStamp.OutputFile = outputPdf;  // destination PDF

        // ------------------------------------------------------------
        // 2. Create a single Stamp instance that will be applied to
        //    every page (Pages = null) as a background image.
        // ------------------------------------------------------------
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();

        // Bind the image that will be used as the watermark
        stamp.BindImage(watermarkImage);

        // Make the stamp a background element (appears behind page content)
        stamp.IsBackground = true;

        // Optional visual settings
        stamp.Opacity = 0.3f;                 // semi‑transparent
        stamp.SetImageSize(150, 150);         // size of the watermark on the page
        stamp.SetOrigin(0, 0);                // position (lower‑left corner)

        // Pages = null means the stamp is applied to all pages.
        // No need to set it explicitly, but shown here for clarity.
        stamp.Pages = null;

        // ------------------------------------------------------------
        // 3. Add the stamp to the document and finalize
        // ------------------------------------------------------------
        fileStamp.AddStamp(stamp);
        fileStamp.Close();    // Saves the output file and releases resources

        Console.WriteLine($"Watermarked PDF saved to '{outputPdf}'.");
    }
}