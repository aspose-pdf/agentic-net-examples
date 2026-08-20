using System;
using System.IO;
using System.Drawing; // System.Drawing.Color for FormattedText
using Aspose.Pdf.Facades; // PdfFileMend, FormattedText, EncodingType

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "stamped_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Prepare the formatted text (watermark) – use System.Drawing.Color and a float font size.
        FormattedText formattedText = new FormattedText(
            "CONFIDENTIAL",          // text
            Color.Red,                // System.Drawing.Color
            "Helvetica",             // font name
            EncodingType.Winansi,     // encoding
            false,                    // embed font?
            36f);                     // font size (float)

        // Pages that should receive the stamp.
        int[] targetPages = { 1, 5, 10 };

        // Use PdfFileMend (not PdfFileStamp) to add the text to the selected pages.
        PdfFileMend mend = new PdfFileMend();
        mend.BindPdf(inputPath);
        // AddText overload: (FormattedText, int[] pages, float llx, float lly, float urx, float ury)
        mend.AddText(formattedText, targetPages, 100f, 500f, 300f, 550f);
        mend.Save(outputPath);
        mend.Close();

        Console.WriteLine($"Text stamp applied to pages 1,5,10. Output saved to '{outputPath}'.");
    }
}
