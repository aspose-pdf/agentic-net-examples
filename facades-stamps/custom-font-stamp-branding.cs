using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "branded_output.pdf";
        const string fontPath  = "MyBrandFont.ttf";   // path to custom TrueType font
        const string stampText = "My Brand";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(fontPath))
        {
            Console.Error.WriteLine($"Font file not found: {fontPath}");
            return;
        }

        try
        {
            // Locate the custom font and obtain its internal font name
            var fontInfo = FontRepository.FindFont(fontPath);
            if (fontInfo == null)
                throw new InvalidOperationException($"Unable to load font from '{fontPath}'.");
            string fontName = fontInfo.FontName;

            // Create formatted text that uses the custom font
            var formatted = new FormattedText(
                stampText,
                System.Drawing.Color.Blue,   // fully qualified to avoid ambiguity
                fontName,
                EncodingType.Winansi,
                false,
                36); // font size

            // Build a stamp that contains the formatted text
            var stamp = new Aspose.Pdf.Facades.Stamp();
            stamp.BindLogo(formatted);

            // Load the source PDF and apply the stamp to every page
            var pdfStamp = new PdfFileStamp();
            pdfStamp.BindPdf(inputPdf);
            pdfStamp.AddStamp(stamp);
            pdfStamp.Save(outputPdf);

            Console.WriteLine($"Stamped PDF saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
