using System;
using System.IO;
using Aspose.Pdf.Facades;   // PdfFileStamp and Stamp classes

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";      // source PDF
        const string outputPdf = "output.pdf";     // stamped PDF
        const string imageFile = "stampImage.png"; // image to use as stamp

        // Verify required files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(imageFile))
        {
            Console.Error.WriteLine($"Image file not found: {imageFile}");
            return;
        }

        // ------------------------------------------------------------
        // Create the PdfFileStamp facade and configure input/output files
        // ------------------------------------------------------------
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.InputFile  = inputPdf;
        fileStamp.OutputFile = outputPdf;

        // ------------------------------------------------------------
        // Create a Stamp object, bind the image and set its properties
        // ------------------------------------------------------------
        Stamp stamp = new Stamp();

        // Bind the external image file to the stamp
        stamp.BindImage(imageFile);

        // Position the stamp on the page (origin is lower‑left corner)
        stamp.SetOrigin(100, 500);          // X = 100, Y = 500 (points)

        // Define the size of the image stamp
        stamp.SetImageSize(150, 100);       // width = 150, height = 100 (points)

        // Make the stamp semi‑transparent
        stamp.Opacity = 0.7f;

        // Place the stamp in the foreground (false = on top of page content)
        stamp.IsBackground = false;

        // Apply the stamp only to the second page (pages are 1‑based)
        stamp.PageNumber = 2;

        // ------------------------------------------------------------
        // Add the configured stamp to the PDF and save the result
        // ------------------------------------------------------------
        fileStamp.AddStamp(stamp);
        fileStamp.Close();   // Persist changes and release resources
    }
}