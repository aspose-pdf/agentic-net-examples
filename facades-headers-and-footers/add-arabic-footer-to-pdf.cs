using System;
using System.IO;
using System.Drawing;
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

        // Load the PDF into PdfFileStamp
        using (PdfFileStamp stamp = new PdfFileStamp())
        {
            stamp.BindPdf(inputPath);

            // Prepare formatted Arabic footer text using the FormattedText constructor.
            // Use a Unicode font that contains Arabic glyphs (e.g., "Arial Unicode MS").
            FormattedText footer = new FormattedText(
                "سري",                     // Arabic word "Secret"
                Color.Black,               // text colour
                "Arial Unicode MS",       // font name that supports Arabic
                EncodingType.Winansi,      // encoding (Winansi works for Unicode fonts)
                false,                     // embed flag (false = not embedded)
                12);                       // font size

            // Add the footer with a bottom margin of 10 points.
            // Aspose.Pdf automatically renders right‑to‑left text when a suitable Unicode font is used.
            stamp.AddFooter(footer, 10);

            // Save the modified PDF
            stamp.Save(outputPath);
        }

        Console.WriteLine($"Footer added and saved to '{outputPath}'.");
    }
}
