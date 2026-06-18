using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // for EncodingType

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Initialize the PdfFileStamp facade
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPdf);

        // Create a text stamp (logo) with desired appearance
        // FormattedText constructor: (text, System.Drawing.Color, fontName, encoding, isEmbedded, fontSize)
        FormattedText ft = new FormattedText(
            "DIAGONAL",                     // stamp text
            System.Drawing.Color.Red,       // text color (System.Drawing.Color is required)
            "Helvetica",                    // font name
            EncodingType.Winansi,           // encoding
            false,                          // embed font?
            48);                            // font size

        // Create the stamp and bind the formatted text
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
        stamp.BindLogo(ft);

        // Apply a 45‑degree rotation for diagonal placement
        stamp.Rotation = 45f;

        // Add the stamp to the PDF
        fileStamp.AddStamp(stamp);

        // Save the result and release resources
        fileStamp.Save(outputPdf);
        fileStamp.Close();

        Console.WriteLine($"Aspose.Pdf.Facades.Stamp with 45° rotation applied and saved to '{outputPdf}'.");
    }
}