using System;
using System.IO;
using System.Drawing; // Required for Color
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "stamped_output.pdf";
        const string stampText = "CONFIDENTIAL";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create formatted text for the stamp – fully‑qualified to avoid ambiguity
        Aspose.Pdf.Facades.FormattedText formatted = new Aspose.Pdf.Facades.FormattedText(
            stampText,                     // text
            System.Drawing.Color.Red,      // text color (System.Drawing.Color required)
            "Helvetica",                 // font name
            Aspose.Pdf.Facades.EncodingType.Winansi, // encoding
            false,                         // embed font
            48f);                          // font size (float)

        // Configure the stamp – use fully‑qualified Stamp type
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
        stamp.BindLogo(formatted);   // bind the formatted text to the stamp
        stamp.IsBackground = true;   // place stamp behind page content
        stamp.Opacity = 0.5f;         // semi‑transparent
        stamp.Pages = new int[] { 1, 5, 10 }; // apply only to pages 1, 5 and 10

        // Apply the stamp using PdfFileStamp facade
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPath);   // load source PDF
        fileStamp.AddStamp(stamp);      // add the configured stamp
        fileStamp.Save(outputPath);     // save the result
        fileStamp.Close();              // release resources

        Console.WriteLine($"Aspose.Pdf.Facades.Stamp applied to pages 1, 5, and 10. Output saved to '{outputPath}'.");
    }
}