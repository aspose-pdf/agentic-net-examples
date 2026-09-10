using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // for FormattedText and EncodingType

class Program
{
    static void Main()
    {
        // Paths for the source PDF, logo image and the output PDF
        const string inputPdf  = "input.pdf";
        const string logoImage = "logo.png";
        const string outputPdf = "output.pdf";

        // Verify that required files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(logoImage))
        {
            Console.Error.WriteLine($"Logo image not found: {logoImage}");
            return;
        }

        // Create a PdfFileStamp facade and specify input/output files
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.InputFile  = inputPdf;
        fileStamp.OutputFile = outputPdf;

        // Create a Aspose.Pdf.Facades.Stamp object that will hold both image and text
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();

        // ----- Image part -----
        // Bind the logo image to the stamp
        stamp.BindImage(logoImage);
        // Position the image (origin) and set its size
        stamp.SetOrigin(100, 700);          // X, Y coordinates (bottom‑left origin)
        stamp.SetImageSize(100, 100);       // Width, Height in points
        stamp.Opacity = 0.9f;               // Slightly transparent
        stamp.IsBackground = false;         // Draw on top of page content

        // ----- Text part -----
        // Create formatted text for the custom caption
        FormattedText ft = new FormattedText(
            "Confidential Document",          // Text
            System.Drawing.Color.Red,         // Text color (System.Drawing.Color is required)
            "Helvetica",                      // Font name
            EncodingType.Winansi,             // Encoding
            false,                            // Embedded flag
            24);                              // Font size

        // Bind the formatted text to the same stamp
        stamp.BindLogo(ft);

        // Add the combined stamp to the PDF file
        fileStamp.AddStamp(stamp);

        // Close the facade to finalize and write the output PDF
        fileStamp.Close();

        Console.WriteLine($"Aspose.Pdf.Facades.Stamp with image and text saved to '{outputPdf}'.");
    }
}