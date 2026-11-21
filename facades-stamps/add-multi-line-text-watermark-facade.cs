using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "watermarked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Build a FormattedText object that contains all watermark lines.
        var formattedText = new FormattedText(
            "Confidential",                     // first line
            System.Drawing.Color.Red,            // colour – fully qualified to avoid ambiguity
            "Helvetica",                        // font name
            EncodingType.Winansi,                // encoding
            false,                               // embed the font?
            36);                                 // font size

        // Add the remaining lines.
        formattedText.AddNewLineText("Do Not Distribute");
        formattedText.AddNewLineText("© Company Name");

        // Create a Stamp and bind the FormattedText to it.
        var textStamp = new Aspose.Pdf.Facades.Stamp(); // fully qualified to avoid ambiguity with Aspose.Pdf.Stamp
        textStamp.BindLogo(formattedText);
        textStamp.SetOrigin(100, 500);   // lower‑left corner of the first line
        textStamp.Opacity = 0.5f;        // 50 % transparent
        textStamp.IsBackground = false; // draw on top of page content

        // Apply the stamp to every page of the source PDF.
        using (var fileStamp = new PdfFileStamp())
        {
            fileStamp.BindPdf(inputPath);
            fileStamp.AddStamp(textStamp); // default = all pages
            fileStamp.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}
