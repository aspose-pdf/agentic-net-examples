using System;
using System.IO;
using Aspose.Pdf.Facades; // PdfFileStamp, Stamp, FormattedText, EncodingType

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string stampText  = "CONFIDENTIAL";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create formatted text for the stamp (text, color, font, encoding, embedded, size)
        FormattedText formattedText = new FormattedText(
            stampText,
            System.Drawing.Color.Red,   // text color (System.Drawing.Color is required here)
            "Helvetica",                // font name
            EncodingType.Winansi,       // encoding
            false,                      // not embedded
            36);                        // font size

        // Configure the stamp
        Stamp stamp = new Stamp();
        stamp.BindLogo(formattedText);   // set the text as stamp content
        stamp.SetOrigin(100f, 400f);     // position on the page (optional)
        stamp.Rotation = 45f;            // rotate 45 degrees for diagonal placement
        stamp.IsBackground = true;      // place behind page content (optional)
        stamp.Opacity = 0.5f;            // semi‑transparent (optional)

        // Apply the stamp to the PDF using the PdfFileStamp facade
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPath);    // load source PDF
        fileStamp.AddStamp(stamp);       // add the configured stamp (applies to all pages)
        fileStamp.Save(outputPath);      // persist changes
        fileStamp.Close();               // release resources

        Console.WriteLine($"Stamp with 45° rotation applied and saved to '{outputPath}'.");
    }
}