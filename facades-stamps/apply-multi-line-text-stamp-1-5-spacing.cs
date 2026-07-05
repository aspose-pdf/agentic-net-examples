using System;
using System.IO;
using Aspose.Pdf.Facades; // for PdfFileStamp, Stamp, FormattedText, EncodingType

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

        // Create a formatted text object for the multi‑line watermark.
        // First line is supplied via the constructor; additional lines are added
        // with a line‑spacing of 1.5 points using AddNewLineText.
        Aspose.Pdf.Facades.FormattedText ft = new Aspose.Pdf.Facades.FormattedText(
            "CONFIDENTIAL",                                 // first line text
            System.Drawing.Color.Red,                       // text color (System.Drawing is required here)
            "Helvetica",                                    // font name
            Aspose.Pdf.Facades.EncodingType.Winansi,        // encoding
            false,                                          // embed font?
            36);                                            // font size

        // Add a second line with extra spacing (1.5 points) for readability.
        ft.AddNewLineText("Do Not Distribute", 1.5f);

        // Create a stamp, bind the formatted text, and configure appearance.
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
        stamp.BindLogo(ft);                 // attach the text to the stamp
        stamp.IsBackground = true;          // place behind page content (optional)
        stamp.Opacity = 0.5f;                // semi‑transparent
        stamp.SetOrigin(100, 400);           // position on the page (X, Y)

        // Use PdfFileStamp facade to apply the stamp to all pages.
        using (Aspose.Pdf.Facades.PdfFileStamp fileStamp = new Aspose.Pdf.Facades.PdfFileStamp())
        {
            fileStamp.BindPdf(inputPdf);     // load source PDF
            fileStamp.AddStamp(stamp);       // add the prepared stamp
            fileStamp.Save(outputPdf);       // write the result
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPdf}'.");
    }
}