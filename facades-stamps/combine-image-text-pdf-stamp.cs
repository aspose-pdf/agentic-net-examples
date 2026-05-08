using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // FormattedText, EncodingType

class Program
{
    static void Main()
    {
        // Paths for source PDF, logo image, and output PDF
        const string inputPdf  = "input.pdf";
        const string logoImage = "logo.png";
        const string outputPdf = "output.pdf";

        // Verify required files exist
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

        // Initialize PdfFileStamp facade and specify input/output files
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.InputFile  = inputPdf;
        fileStamp.OutputFile = outputPdf;

        // Create a stamp instance
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();

        // Bind the logo image to the stamp
        stamp.BindImage(logoImage);

        // Prepare formatted text for the stamp
        FormattedText formattedText = new FormattedText(
            "Custom Text",                     // Text content
            System.Drawing.Color.Black,        // Text color (System.Drawing.Color is required)
            "Helvetica",                       // Font name
            EncodingType.Winansi,              // Encoding
            false,                             // IsEmbedded (false = use system font)
            12);                               // Font size

        // Bind the formatted text to the same stamp
        stamp.BindLogo(formattedText);

        // Optional: set stamp appearance (position, opacity, background)
        stamp.SetOrigin(100, 150);   // X, Y coordinates (bottom‑left origin)
        stamp.Opacity = 0.8f;        // Semi‑transparent
        stamp.IsBackground = false; // Place stamp on top of page content

        // Add the combined image‑and‑text stamp to the PDF
        fileStamp.AddStamp(stamp);

        // Finalize and save the output PDF
        fileStamp.Close();

        Console.WriteLine($"Stamp with logo and text saved to '{outputPdf}'.");
    }
}