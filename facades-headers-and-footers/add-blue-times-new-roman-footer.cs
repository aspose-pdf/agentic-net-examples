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
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create a PdfFileStamp instance (does NOT implement IDisposable)
        PdfFileStamp stamp = new PdfFileStamp();

        // Bind the existing PDF document to the stamp object
        stamp.BindPdf(inputPath);

        // Prepare formatted text for the footer.
        // Use fully‑qualified System.Drawing.Color to avoid ambiguity.
        // Use Aspose.Pdf.Facades.FontStyle (TimesRoman) – the constructor expects a FontStyle, not a font name string.
        // The constructor signature is:
        //   FormattedText(string text, System.Drawing.Color textColor, FontStyle fontStyle,
        //                 EncodingType encoding, bool embedFont, float fontSize, float lineSpacing)
        FormattedText footer = new FormattedText(
            "Confidential – Footer Text",          // text
            System.Drawing.Color.Blue,               // text color (System.Drawing.Color)
            FontStyle.TimesRoman,                    // Times New Roman style
            EncodingType.Winansi,                    // encoding
            false,                                   // embed font flag
            12f,                                     // font size
            0f);                                     // line spacing (0 = default)

        // Add the footer to every page with a bottom margin of 30 points.
        // AddFooter centers the text horizontally by default, so no extra alignment property is needed.
        stamp.AddFooter(footer, 30f);

        // Save the modified PDF
        stamp.Save(outputPath);

        // Release resources held by PdfFileStamp
        stamp.Close();

        Console.WriteLine($"PDF saved with footer to '{outputPath}'.");
    }
}
