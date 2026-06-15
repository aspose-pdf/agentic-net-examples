using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

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

        // Create a text stamp using FormattedText (requires System.Drawing.Color)
        Aspose.Pdf.Facades.FormattedText formattedText = new Aspose.Pdf.Facades.FormattedText(
            "CONFIDENTIAL",                     // text
            System.Drawing.Color.Red,           // text color
            "Helvetica",                        // font name
            EncodingType.Winansi,               // encoding
            false,                              // embed font?
            36);                                // font size

        // Configure the stamp
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
        stamp.BindLogo(formattedText);          // bind the text to the stamp
        stamp.IsBackground = true;              // place stamp behind page content
        stamp.Pages = new int[] { 1, 5, 10 };    // apply only to pages 1, 5 and 10

        // Apply the stamp using PdfFileStamp facade
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPdf);             // load source PDF
        fileStamp.AddStamp(stamp);               // add the configured stamp
        fileStamp.Save(outputPdf);               // save result
        fileStamp.Close();                       // release resources

        Console.WriteLine($"Stamp applied to pages 1, 5, and 10. Output saved as '{outputPdf}'.");
    }
}