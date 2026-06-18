using System;
using System.IO;
using Aspose.Pdf.Facades; // Facade classes for stamping
using System.Drawing; // for Color

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "watermarked.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Bind the source PDF to a PdfFileStamp facade
        PdfFileStamp pdfStamp = new PdfFileStamp();
        pdfStamp.BindPdf(inputPdf);

        // ----- First line (largest font) -----
        var ft1 = new FormattedText(
            "CONFIDENTIAL",               // text
            Color.Red,                     // text color
            "Helvetica",                  // font name
            EncodingType.Winansi,          // encoding
            false,                         // embed font?
            48);                           // font size
        var stamp1 = new Stamp();
        stamp1.BindLogo(ft1);
        stamp1.SetOrigin(100, 750); // X, Y coordinates (points from bottom‑left)
        stamp1.Opacity = 0.5f;       // semi‑transparent
        pdfStamp.AddStamp(stamp1);

        // ----- Second line (medium font) -----
        var ft2 = new FormattedText(
            "Do Not Distribute",
            Color.Red,
            "Helvetica",
            EncodingType.Winansi,
            false,
            36);
        var stamp2 = new Stamp();
        stamp2.BindLogo(ft2);
        stamp2.SetOrigin(100, 700);
        stamp2.Opacity = 0.5f;
        pdfStamp.AddStamp(stamp2);

        // ----- Third line (smallest font) -----
        var ft3 = new FormattedText(
            "Company Internal Use Only",
            Color.Red,
            "Helvetica",
            EncodingType.Winansi,
            false,
            24);
        var stamp3 = new Stamp();
        stamp3.BindLogo(ft3);
        stamp3.SetOrigin(100, 650);
        stamp3.Opacity = 0.5f;
        pdfStamp.AddStamp(stamp3);

        // Save the watermarked PDF and release resources
        pdfStamp.Save(outputPdf);
        pdfStamp.Close();

        Console.WriteLine($"Watermarked PDF saved to '{outputPdf}'.");
    }
}
