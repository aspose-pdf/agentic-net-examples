using System;
using System.IO;
using Aspose.Pdf.Facades;   // PdfFileStamp, Stamp, FormattedText, EncodingType

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "protected.pdf";

        // Pages on which the stamp should appear (1‑based indexing)
        int[] selectedPages = new int[] { 1, 3, 5 }; // adjust as needed

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create the text that will be used as a watermark.
        // FormattedText constructor requires System.Drawing.Color for the text color.
        FormattedText ft = new FormattedText(
            "CONFIDENTIAL",                     // text
            System.Drawing.Color.Red,           // text color
            "Helvetica",                        // font name
            EncodingType.Winansi,               // encoding
            false,                              // embed font?
            48);                                // font size

        // Configure the stamp.
        Stamp stamp = new Stamp();
        stamp.BindLogo(ft);          // bind the formatted text as stamp content
        stamp.Opacity = 0.7f;        // 70% opacity (0.0 = fully transparent, 1.0 = opaque)
        stamp.IsBackground = true;  // place stamp behind page content (watermark effect)
        stamp.Pages = selectedPages; // apply only to the selected pages; null = all pages

        // Apply the stamp to the PDF using PdfFileStamp.
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPath);   // load source PDF
        fileStamp.AddStamp(stamp);      // add the configured stamp
        fileStamp.Save(outputPath);     // write the result
        fileStamp.Close();              // release resources (PdfFileStamp does not implement IDisposable)

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}