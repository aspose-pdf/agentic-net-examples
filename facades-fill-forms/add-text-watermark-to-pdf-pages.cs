using System;
using System.IO;
using Aspose.Pdf;                     // Core PDF classes (Document, Page, etc.)
using Aspose.Pdf.Facades;            // Facade classes (PdfFileStamp, Stamp, FormattedText)

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string filledPdfPath   = "filled_form.pdf";   // PDF after AutoFiller processing
        const string watermarkedPath = "watermarked_output.pdf";

        // Verify input file exists
        if (!File.Exists(filledPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {filledPdfPath}");
            return;
        }

        // -----------------------------------------------------------------
        // Add a text watermark to every page using PdfFileStamp (Facade API)
        // -----------------------------------------------------------------
        // 1. Create the stamp (text) – FormattedText constructor sets text,
        //    color (System.Drawing.Color), font name, encoding, embed flag, size.
        Aspose.Pdf.Facades.FormattedText ft = new Aspose.Pdf.Facades.FormattedText(
            "CONFIDENTIAL",                     // watermark text
            System.Drawing.Color.Red,           // text color (System.Drawing required)
            "Helvetica",                        // font name
            Aspose.Pdf.Facades.EncodingType.Winansi,
            false,                              // embed font?
            72);                                // font size

        // 2. Create a Stamp object and bind the formatted text.
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
        stamp.BindLogo(ft);                     // use the text as the stamp content
        stamp.IsBackground = true;              // place behind page content
        stamp.Opacity = 0.3f;                   // semi‑transparent
        // Position the stamp (centered). Origin is the lower‑left corner of the stamp.
        // Here we place it roughly at the center of a typical A4 page (595x842 points).
        stamp.SetOrigin(200f, 400f);
        // Optional: set size of the stamp (width, height). If not set, size is derived from text.
        // stamp.SetImageSize(300f, 100f);

        // 3. Use PdfFileStamp to apply the stamp to the whole document.
        using (Aspose.Pdf.Facades.PdfFileStamp fileStamp = new Aspose.Pdf.Facades.PdfFileStamp())
        {
            // Bind the source PDF (the one already filled by AutoFiller)
            fileStamp.BindPdf(filledPdfPath);

            // Add the prepared stamp – by default it applies to all pages.
            fileStamp.AddStamp(stamp);

            // Save the result to a new file.
            fileStamp.Save(watermarkedPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{watermarkedPath}'.");
    }
}