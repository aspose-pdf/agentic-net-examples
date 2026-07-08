using System;
using System.IO;
using Aspose.Pdf.Facades; // Facade API for stamping

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
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(stampImg))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImg}");
            return;
        }

        // Initialize the facade and specify input/output files
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.InputFile  = inputPdf;
        fileStamp.OutputFile = outputPdf;

        // Create a Stamp object (facade version)
        Stamp stamp = new Stamp();

        // Bind the image that will be used as the stamp content
        stamp.BindImage(stampImg);

        // Explicitly set IsBackground to false so the stamp overlays page content
        stamp.IsBackground = false; // default is false; set for clarity

        // Optional: configure position, size, and opacity
        stamp.SetOrigin(100, 500);      // X and Y coordinates on the page
        stamp.SetImageSize(150, 100);   // Width and height of the stamp
        stamp.Opacity = 0.8f;           // Semi‑transparent

        // Add the stamp to all pages (Pages = null means all pages)
        fileStamp.AddStamp(stamp);

        // Finalize and write the output PDF
        fileStamp.Close();

        Console.WriteLine($"Stamp applied and saved to '{outputPdf}'.");
    }
}