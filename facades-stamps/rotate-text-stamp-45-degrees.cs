using System;
using System.Drawing;                     // For System.Drawing.Color used by FormattedText
using Aspose.Pdf.Facades;                // Facade classes: PdfFileStamp, Stamp, FormattedText

class RotateTextStampExample
{
    static void Main()
    {
        // Paths to the source PDF and the output PDF
        const string inputPdf  = "input.pdf";
        const string outputPdf = "rotated_stamp.pdf";

        // Ensure the input file exists
        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Initialize the PdfFileStamp facade with input and output files
        // (PdfFileStamp does NOT implement IDisposable, so we call Close() explicitly)
        PdfFileStamp fileStamp = new PdfFileStamp(inputPdf, outputPdf);

        // Create a text stamp
        Stamp stamp = new Stamp();

        // Bind a formatted text to the stamp.
        // FormattedText constructor: (text, color, fontName, encoding, isEmbedded, fontSize)
        FormattedText formatted = new FormattedText(
            "CONFIDENTIAL",               // Text to display
            Color.Red,                    // Text color (System.Drawing.Color)
            "Helvetica",                  // Font name
            EncodingType.Winansi,         // Encoding
            false,                        // Do not embed the font
            48);                          // Font size

        stamp.BindLogo(formatted);

        // Set the rotation angle to 45 degrees (around the stamp's center)
        stamp.Rotation = 45f;

        // Optionally set the position of the stamp on the page (origin is lower‑left corner)
        // Here we place it near the center of a typical A4 page (595x842 points)
        stamp.SetOrigin(200f, 400f);

        // Add the stamp to all pages of the document
        fileStamp.AddStamp(stamp);

        // Finalize and save the output PDF
        fileStamp.Close();

        Console.WriteLine($"Stamp with 45° rotation applied and saved to '{outputPdf}'.");
    }
}