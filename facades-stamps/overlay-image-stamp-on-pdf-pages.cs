using System;
using System.IO;
using Aspose.Pdf.Facades;   // PdfFileStamp and Stamp are in this namespace

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // source PDF
        const string outputPdf = "output.pdf";  // destination PDF
        const string stampImg  = "stamp.png";   // image to use as stamp

        // Verify required files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }
        if (!File.Exists(stampImg))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImg}");
            return;
        }

        // Initialize the facade for stamping
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.InputFile  = inputPdf;   // set source PDF
        fileStamp.OutputFile = outputPdf;  // set output PDF

        // Create a stamp instance
        Stamp stamp = new Stamp();

        // Bind an image to the stamp (could also bind text or PDF page)
        stamp.BindImage(stampImg);

        // Explicitly set IsBackground to false so the stamp overlays page content
        stamp.IsBackground = false;

        // Optional: position, size and opacity of the stamp
        stamp.SetOrigin(100, 500);      // X and Y coordinates (bottom‑left origin)
        stamp.SetImageSize(150, 100);   // width and height
        stamp.Opacity = 0.8f;           // semi‑transparent

        // Apply the stamp to all pages of the document
        fileStamp.AddStamp(stamp);

        // Finalize and write the output file
        fileStamp.Close();

        Console.WriteLine($"Stamp applied and saved to '{outputPdf}'.");
    }
}