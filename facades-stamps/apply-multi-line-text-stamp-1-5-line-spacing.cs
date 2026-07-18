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

        // Create formatted text with multiple lines.
        // First line is added via constructor, subsequent lines via AddNewLineText with line spacing.
        FormattedText formatted = new FormattedText(
            "Confidential",                     // first line text
            System.Drawing.Color.Red,           // text color (System.Drawing.Color is required here)
            "Helvetica",                        // font name
            EncodingType.Winansi,               // encoding
            false,                              // embed font
            36);                                // font size

        // Add additional lines; the second parameter is the extra line spacing (in points).
        // Using 1.5f as the additional spacing to achieve a 1.5 line‑spacing effect.
        formatted.AddNewLineText("Do Not Distribute", 1.5f);
        formatted.AddNewLineText("Authorized Personnel Only", 1.5f);

        // Create a stamp and bind the formatted text to it.
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
        stamp.BindLogo(formatted);          // bind the multi‑line text
        stamp.IsBackground = true;          // place behind page content
        stamp.Opacity = 0.5f;                // semi‑transparent
        stamp.SetOrigin(100, 400);           // position on the page (X, Y)

        // Apply the stamp to the entire PDF using PdfFileStamp.
        PdfFileStamp pdfStamp = new PdfFileStamp();
        pdfStamp.BindPdf(inputPdf);          // load source PDF
        pdfStamp.AddStamp(stamp);            // add the prepared stamp
        pdfStamp.Save(outputPdf);            // save the result
        pdfStamp.Close();                    // release resources

        Console.WriteLine($"Watermarked PDF saved to '{outputPdf}'.");
    }
}