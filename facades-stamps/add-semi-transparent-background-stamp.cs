using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;   // required for FormattedText and EncodingType

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

        // Create a text stamp that will be placed as a background.
        // Opacity 0.3 = 30% transparency.
        // BindLogo attaches formatted text to the stamp.
        FormattedText ft = new FormattedText(
            "BACKGROUND",                     // text to display
            System.Drawing.Color.Gray,        // text color (System.Drawing is required here)
            "Helvetica",                      // font name
            EncodingType.Winansi,             // encoding
            false,                            // embed font?
            72);                              // font size

        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
        stamp.IsBackground = true;   // place behind page content
        stamp.Opacity      = 0.3f;   // 30% opacity
        stamp.BindLogo(ft);          // use the formatted text as the stamp content
        stamp.SetOrigin(100, 400);   // position of the stamp (optional)
        stamp.SetImageSize(300, 200); // size of the stamp (optional)

        // Use the PdfFileStamp facade to apply the stamp to the whole document.
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPdf);          // load source PDF
        fileStamp.AddStamp(stamp);            // add the configured stamp
        fileStamp.Save(outputPdf);            // write result
        fileStamp.Close();                    // release resources

        Console.WriteLine($"Background stamp applied. Output saved to '{outputPdf}'.");
    }
}