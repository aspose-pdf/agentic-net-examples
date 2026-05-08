using System;
using System.IO;
using Aspose.Pdf.Facades;               // PdfFileStamp, Stamp, FormattedText, EncodingType
using System.Drawing;                  // System.Drawing.Color (required by FormattedText)

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";      // source PDF
        const string outputPdf = "watermarked.pdf"; // result PDF

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Create a multi‑line formatted text with custom line spacing (1.5 points extra)
        FormattedText ft = new FormattedText(
            "Confidential",                     // first line
            Color.Black,                        // text color
            "Helvetica",                        // font name
            EncodingType.Winansi,               // encoding
            false,                              // embedded flag
            24);                                // font size

        // Add subsequent lines; the second argument is additional line spacing
        ft.AddNewLineText("Do Not Distribute", 1.5f);
        ft.AddNewLineText("Internal Use Only", 1.5f);

        // Configure the stamp
        Stamp stamp = new Stamp();
        stamp.BindLogo(ft);                     // bind the formatted text
        stamp.IsBackground = true;              // place behind page content (optional)
        stamp.Opacity = 0.5f;                    // semi‑transparent (optional)
        stamp.SetOrigin(100, 400);               // position on the page (X, Y)

        // Apply the stamp to all pages of the PDF
        using (PdfFileStamp pdfStamp = new PdfFileStamp())
        {
            pdfStamp.BindPdf(inputPdf);          // load source PDF
            pdfStamp.AddStamp(stamp);            // add the configured stamp
            pdfStamp.Save(outputPdf);            // write the result
            pdfStamp.Close();                    // finalize
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPdf}'.");
    }
}