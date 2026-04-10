using System;
using System.IO;
using Aspose.Pdf.Facades;          // Facade classes (PdfFileStamp, Stamp)
using Aspose.Pdf.Text;            // FormattedText, EncodingType

class RotateTextAnnotation
{
    static void Main()
    {
        // Input and output PDF paths
        const string inputPdf  = "input.pdf";
        const string outputPdf = "rotated_annotation.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Create a PdfFileStamp facade – this class does NOT implement IDisposable,
        // so we do NOT wrap it in a using block.
        PdfFileStamp fileStamp = new PdfFileStamp(inputPdf, outputPdf);

        // Create a Stamp object which will act as a text annotation.
        // FormattedText constructor sets the text, color, font, encoding, embed flag, and size.
        FormattedText ft = new FormattedText(
            "Rotated Text",                     // Text to display
            System.Drawing.Color.Black,         // Text color (System.Drawing.Color is required here)
            "Helvetica",                        // Font name
            EncodingType.Winansi,               // Encoding
            false,                              // Do not embed the font
            12);                                // Font size

        // Bind the formatted text to the stamp.
        Stamp stamp = new Stamp();
        stamp.BindLogo(ft);

        // Rotate the stamp by 90 degrees.
        stamp.Rotation = 90f;

        // Add the stamp (text annotation) to the PDF.
        fileStamp.AddStamp(stamp);

        // Finalize and close the facade.
        fileStamp.Close();

        Console.WriteLine($"Rotated text annotation added and saved to '{outputPdf}'.");
    }
}