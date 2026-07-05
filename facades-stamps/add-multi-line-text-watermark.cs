using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

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

        // Create a FormattedText object and add multiple lines using AddNewLineText
        // Parameters: text, text color, font name, encoding, isEmbedded, font size
        FormattedText ft = new FormattedText(
            "Confidential",                     // first line
            System.Drawing.Color.Red,           // text color
            "Helvetica",                        // font
            EncodingType.Winansi,               // encoding
            false,                              // not embedded
            48);                                // font size

        // Add additional lines
        ft.AddNewLineText("Do Not Distribute");
        ft.AddNewLineText("Company XYZ");

        // Use PdfFileStamp (a Facades class) to add the text as a header.
        // The header will appear on every page; you can treat it as a watermark.
        PdfFileStamp stamp = new PdfFileStamp();
        stamp.BindPdf(inputPdf);                 // load the source PDF
        stamp.AddHeader(ft, 0);                  // add the multi‑line text at the top (offset 0)

        // Save the result
        stamp.Save(outputPdf);
        stamp.Close();

        Console.WriteLine($"Watermarked PDF saved to '{outputPdf}'.");
    }
}