using System;
using System.IO;
using System.Drawing;                     // System.Drawing.Color is required for FormattedText
using Aspose.Pdf.Facades;                // Facade classes for stamping
using Aspose.Pdf.Text;                    // EncodingType enum for FormattedText

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

        // Create a Facade stamp with multiline text, Arial 14pt, blue color
        FormattedText formatted = new FormattedText(
            "First line of text\nSecond line of text\nThird line of text", // multiline content
            Color.Blue,               // text color (System.Drawing.Color)
            "Arial",                  // font name
            EncodingType.Winansi,     // encoding
            false,                    // embed font? false = use system font
            14);                      // font size

        // Configure the stamp
        Stamp stamp = new Stamp();
        stamp.BindLogo(formatted);   // bind the formatted text to the stamp
        stamp.IsBackground = false; // stamp will appear on top of page content
        stamp.SetOrigin(100, 500);  // position of the stamp (optional)

        // Apply the stamp to the PDF using PdfFileStamp facade
        using (PdfFileStamp fileStamp = new PdfFileStamp())
        {
            fileStamp.BindPdf(inputPdf);   // load source PDF
            fileStamp.AddStamp(stamp);     // add the prepared stamp
            fileStamp.Save(outputPdf);     // save the stamped PDF
        }

        Console.WriteLine($"Text stamp applied and saved to '{outputPdf}'.");
    }
}