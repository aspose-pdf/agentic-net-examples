using System;
using System.IO;
using System.Drawing; // required for Color
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string logoPath = "logo.png";
        const string customText = "My Company";

        // Validate input files
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(logoPath))
        {
            Console.Error.WriteLine($"Logo image not found: {logoPath}");
            return;
        }

        // Bind the source PDF to the stamp facade
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPdf);

        // Create a stamp instance
        Stamp stamp = new Stamp();

        // Bind the logo image to the stamp
        stamp.BindImage(logoPath);

        // Create formatted text for the custom caption using the correct constructor signature
        // (text, color, fontName, encoding, embedFont, fontSize)
        FormattedText ft = new FormattedText(
            customText,
            Color.Black,               // System.Drawing.Color
            "Helvetica",
            EncodingType.Winansi,
            false,
            12f);

        // Bind the formatted text to the same stamp (combined image‑and‑text stamp)
        // In the Facades API the method is BindLogo for a FormattedText object.
        stamp.BindLogo(ft);

        // Configure stamp appearance
        stamp.SetOrigin(100f, 500f);          // position (X, Y) from bottom‑left of the page
        stamp.SetImageSize(100f, 100f);       // image width and height
        stamp.IsBackground = false;          // place stamp above page content
        stamp.Opacity = 0.8f;                 // semi‑transparent

        // Add the combined stamp to the document
        fileStamp.AddStamp(stamp);

        // Save the resulting PDF
        fileStamp.Save(outputPdf);
        fileStamp.Close();

        Console.WriteLine($"Combined stamp saved to '{outputPdf}'.");
    }
}