using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input and output PDF paths
        const string inputPdf  = "input.pdf";
        const string outputPdf = "stamped_output.pdf";

        // Dynamic values to embed in the stamp
        string author = "John Doe";
        string date   = DateTime.Now.ToString("yyyy-MM-dd");

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Create formatted text with interpolation (all styling via constructor)
        FormattedText formattedText = new FormattedText(
            $"Author: {author}, Date: {date}",
            Color.Black,               // text color
            "Helvetica",              // font name
            EncodingType.Winansi,
            false,                     // embed font?
            12);                       // font size

        // Build a Stamp and bind the formatted text
        Stamp stamp = new Stamp();
        stamp.BindLogo(formattedText);
        stamp.SetOrigin(0, 20);                 // X,Y from bottom‑left corner
        stamp.IsBackground = false;            // place on top of page content
        stamp.Opacity = 0.8f;                   // semi‑transparent
        // Alignment and indent properties are not available on Stamp; positioning is handled via SetOrigin.

        // Apply the stamp to all pages of the document
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPdf);
        fileStamp.AddStamp(stamp);
        fileStamp.Save(outputPdf);
        fileStamp.Close();

        Console.WriteLine($"Stamped PDF saved to '{outputPdf}'.");
    }
}
