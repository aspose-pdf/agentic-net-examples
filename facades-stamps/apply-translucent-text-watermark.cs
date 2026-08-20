using System;
using System.IO;
using System.Text;
using System.Drawing;                     // System.Drawing.Color is required for FormattedText
using Aspose.Pdf.Facades;                // Facade API for stamping

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // source PDF
        const string outputPdf = "output.pdf";  // destination PDF

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Initialize the PdfFileStamp facade and bind the source PDF
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPdf);               // load source document

        // Create a text stamp (watermark) using FormattedText.
        // FormattedText constructor requires System.Drawing.Color for the text color.
        FormattedText ft = new FormattedText(
            "CONFIDENTIAL",                     // watermark text
            Color.Red,                          // text color
            "Helvetica",                        // font name
            EncodingType.Winansi,               // encoding
            false,                              // embed font?
            48);                                // font size

        // Create the stamp object (fully qualified to avoid ambiguity)
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();

        // Bind the formatted text to the stamp
        stamp.BindLogo(ft);

        // Set the stamp to appear as a background (behind page content)
        stamp.IsBackground = true;

        // Set opacity to 50% (0.5). Property type is float.
        stamp.Opacity = 0.5f;

        // By default, Pages = null means the stamp is applied to all pages.
        // Add the stamp to the document.
        fileStamp.AddStamp(stamp);

        // Save the result and release resources.
        fileStamp.Save(outputPdf);
        fileStamp.Close();

        Console.WriteLine($"Translucent watermark applied. Output saved to '{outputPdf}'.");
    }
}