using System;
using System.IO;
using Aspose.Pdf.Facades;          // PdfFileStamp facade
using Aspose.Pdf.Text;            // FormattedText, EncodingType
using System.Drawing;            // System.Drawing.Color required by FormattedText

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "watermarked.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Initialize the PdfFileStamp facade and bind the source PDF
        PdfFileStamp stamp = new PdfFileStamp();
        stamp.BindPdf(inputPdf);

        // Create a FormattedText object for the watermark.
        // Constructor parameters: text, text color, font name, encoding, isEmbedded, font size
        FormattedText ft = new FormattedText(
            "Confidential",                     // first line
            Color.Gray,                         // text color (System.Drawing.Color)
            "Helvetica",                        // font name
            EncodingType.Winansi,               // encoding
            false,                              // embed font?
            48);                                // font size

        // Add additional lines using AddNewLineText
        ft.AddNewLineText("Do Not Distribute");
        ft.AddNewLineText("Company Internal Use Only");

        // Add the multi‑line text as a header (appears on every page).
        // The second argument is the vertical offset from the top of the page.
        stamp.AddHeader(ft, 0);

        // Save the result
        stamp.Save(outputPdf);
        stamp.Close();

        Console.WriteLine($"Watermarked PDF saved to '{outputPdf}'.");
    }
}