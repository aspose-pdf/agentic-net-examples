using System;
using System.IO;
using System.Drawing; // System.Drawing.Color
using Aspose.Pdf.Facades; // FormattedText, EncodingType, Stamp, PdfFileStamp

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

        // Bind the source PDF
        using (PdfFileStamp fileStamp = new PdfFileStamp())
        {
            fileStamp.BindPdf(inputPdf);

            // Create formatted text for the watermark using the constructor (no mutable properties)
            var formattedText = new FormattedText(
                "CONFIDENTIAL\nDO NOT DISTRIBUTE\nFOR INTERNAL USE ONLY",
                System.Drawing.Color.Gray,          // foreground color (System.Drawing.Color)
                "Helvetica",                       // font name
                EncodingType.Winansi,               // encoding
                false,                               // embed the font?
                36);                                 // font size

            // Create a stamp and bind the formatted text
            var stamp = new Aspose.Pdf.Facades.Stamp();
            stamp.BindLogo(formattedText);
            stamp.SetOrigin(100f, 500f);   // Position of the watermark on the page
            stamp.Opacity = 0.5f;          // Semi‑transparent
            stamp.IsBackground = true;    // Render behind page content

            // Add the stamp to all pages of the document
            fileStamp.AddStamp(stamp);

            // Save the result
            fileStamp.Save(outputPdf);
            fileStamp.Close();
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPdf}'.");
    }
}
