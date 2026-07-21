using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for source PDF and output PDF
        const string inputPdf  = "input.pdf";
        const string outputPdf = "watermarked_output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // ---------------------------------------------------------------------
        // 1. Initialise the PdfFileStamp facade (modern API – BindPdf / Save)
        // ---------------------------------------------------------------------
        PdfFileStamp pdfStamp = new PdfFileStamp();
        pdfStamp.BindPdf(inputPdf);

        // ---------------------------------------------------------------------
        // 2. Build the watermark text using FormattedText (all styling via ctor)
        // ---------------------------------------------------------------------
        //   • Text contains line‑breaks (\n) for multiple lines.
        //   • Color is a System.Drawing.Color (Aspose.Pdf.Color is not accepted).
        //   • Font name, encoding and size are supplied in the constructor.
        //   • Custom line height is not directly configurable via a property; the
        //     default line spacing derived from the font size is used. If precise
        //     control is required, create separate stamps per line and position
        //     them individually.
        // ---------------------------------------------------------------------
        FormattedText ft = new FormattedText(
            "CONFIDENTIAL\nDO NOT DISTRIBUTE\nFOR INTERNAL USE ONLY",
            Color.Red,                     // foreground colour (System.Drawing)
            "Helvetica",                  // font name
            EncodingType.Winansi,          // encoding
            false,                         // embed the font? false = use system font
            36);                           // font size (points)
        // Note: FormattedText does not expose a LineSpacing property. The default
        // line spacing based on the font size is applied.

        // ---------------------------------------------------------------------
        // 3. Create a Stamp, bind the FormattedText and configure visual options
        // ---------------------------------------------------------------------
        Stamp stamp = new Stamp();
        stamp.BindLogo(ft);
        // Position the stamp – centre it on the page. SetOrigin expects the lower‑left corner.
        stamp.SetOrigin(100f, 400f); // example coordinates; change as needed
        stamp.Opacity = 0.5f;        // 50 % transparent
        stamp.IsBackground = true;  // render behind existing page content

        // ---------------------------------------------------------------------
        // 4. Add the stamp to every page and save the result
        // ---------------------------------------------------------------------
        pdfStamp.AddStamp(stamp);
        pdfStamp.Save(outputPdf);
        pdfStamp.Close();

        Console.WriteLine($"Watermarked PDF saved to '{outputPdf}'.");
    }
}
