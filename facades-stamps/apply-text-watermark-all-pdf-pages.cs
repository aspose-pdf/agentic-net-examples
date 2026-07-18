using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "stamped_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create a text stamp (watermark)
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();

        // FormattedText constructor sets text, color, font, encoding, embed flag, and size
        FormattedText ft = new FormattedText(
            "CONFIDENTIAL",                 // text
            System.Drawing.Color.Red,       // text color
            "Helvetica",                    // font name
            EncodingType.Winansi,           // encoding
            false,                          // embed font
            48);                            // font size

        stamp.BindLogo(ft);                 // bind the text to the stamp
        stamp.IsBackground = true;          // place stamp behind page content
        stamp.Opacity = 0.5f;               // semi‑transparent
        stamp.Pages = null;                 // null means all pages will be affected

        // Apply the stamp to the whole document using the Facades API
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPath);       // load source PDF
        fileStamp.AddStamp(stamp);          // add the configured stamp
        fileStamp.Save(outputPath);         // write result
        fileStamp.Close();                  // release resources

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}