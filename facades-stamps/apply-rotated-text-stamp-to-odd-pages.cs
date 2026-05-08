using System;
using System.IO;
using System.Linq;
using Aspose.Pdf.Facades;          // PdfFileStamp, Stamp
using Aspose.Pdf.Text;            // FormattedText, EncodingType
using System.Drawing;             // Color (required by FormattedText)

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

        // Initialize the facade and bind the source PDF
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPdf);

        // Create a text stamp
        // FormattedText(string text, Color color, string fontName, EncodingType encoding, bool isEmbedded, int fontSize)
        FormattedText ft = new FormattedText(
            "CONFIDENTIAL",               // stamp text
            Color.Red,                    // text color
            "Helvetica",                  // font
            EncodingType.Winansi,         // encoding
            false,                        // not embedded
            36);                          // font size

        Stamp stamp = new Stamp();
        stamp.BindLogo(ft);               // set the text content
        stamp.Rotation = 30f;             // rotate 30 degrees (arbitrary angle)
        stamp.SetOrigin(10f, 0f);         // place near the left margin, bottom of page

        // Determine odd‑numbered pages
        int pageCount = fileStamp.Document.Pages.Count; // 1‑based page count
        int[] oddPages = Enumerable.Range(1, pageCount)
                                   .Where(p => p % 2 == 1)
                                   .ToArray();

        stamp.Pages = oddPages;           // apply stamp only to odd pages

        // Add the configured stamp to the document
        fileStamp.AddStamp(stamp);

        // Save the result and release resources
        fileStamp.Save(outputPdf);
        fileStamp.Close();

        Console.WriteLine($"Rotated text stamp applied to odd pages. Output saved to '{outputPdf}'.");
    }
}