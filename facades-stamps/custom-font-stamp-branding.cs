using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;
using System.Drawing; // for System.Drawing.Color

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output_branded.pdf";
        const string fontFilePath = "MyBrandFont.ttf";
        const string stampText = "MyBrand";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(fontFilePath))
        {
            Console.Error.WriteLine($"Font file not found: {fontFilePath}");
            return;
        }

        // Load the custom TrueType font so Aspose recognises it.
        Aspose.Pdf.Text.Font customFont = FontRepository.OpenFont(fontFilePath);
        // The font name used in the PDF – usually the file name without extension.
        string fontName = Path.GetFileNameWithoutExtension(fontFilePath);

        // FormattedText expects System.Drawing.Color, not Aspose.Pdf.Color.
        Aspose.Pdf.Facades.FormattedText formattedText = new Aspose.Pdf.Facades.FormattedText(
            stampText,                     // text to display
            System.Drawing.Color.Black,    // text colour
            fontName,                      // font name (must match the TTF family name)
            Aspose.Pdf.Facades.EncodingType.Winansi,
            true,                          // embed the font in the PDF
            48f);                          // font size (float)

        // Create a stamp and bind the formatted text.
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
        stamp.BindLogo(formattedText);
        stamp.IsBackground = false;   // place on top of page content
        stamp.Opacity = 0.7f;          // semi‑transparent
        stamp.SetOrigin(100, 700);     // position (X, Y) in points

        // Use the modern facade API (BindPdf / Save) instead of the obsolete properties.
        Aspose.Pdf.Facades.PdfFileStamp pdfStamp = new Aspose.Pdf.Facades.PdfFileStamp();
        pdfStamp.BindPdf(inputPdfPath);
        pdfStamp.AddStamp(stamp);
        pdfStamp.Save(outputPdfPath);
        pdfStamp.Close();

        Console.WriteLine($"Branded PDF saved to '{outputPdfPath}'.");
    }
}
