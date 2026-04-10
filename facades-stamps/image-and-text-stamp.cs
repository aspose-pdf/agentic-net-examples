using System;
using System.IO;
using System.Drawing;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";      // source PDF
        const string outputPdf = "output.pdf";    // stamped PDF
        const string logoPath = "logo.png";       // image to use as logo
        const string stampText = "Confidential"; // custom text

        // Verify files exist
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

        // Initialize the facade and bind the source PDF
        using (PdfFileStamp fileStamp = new PdfFileStamp())
        {
            fileStamp.BindPdf(inputPdf);   // load source PDF

            // Create a stamp object (fully qualified to avoid ambiguity)
            Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();

            // ----- Image part -----
            // Bind the logo image to the stamp
            stamp.BindImage(logoPath);

            // ----- Text part -----
            // FormattedText constructor sets text, colour, font, encoding, embed flag and size
            FormattedText ft = new FormattedText(
                stampText,
                System.Drawing.Color.Red,   // text colour (System.Drawing.Color)
                "Helvetica",               // font name
                EncodingType.Winansi,
                false,                      // embed font?
                24);                        // font size

            // Bind the formatted text (logo) to the same stamp
            stamp.BindLogo(ft);

            // Position the stamp on the page (coordinates are from bottom‑left)
            stamp.SetOrigin(100, 200);   // X = 100, Y = 200

            // Optional visual settings
            stamp.IsBackground = false; // place stamp on top of page content
            stamp.Opacity = 0.8f;        // semi‑transparent

            // Add the configured stamp to the PDF
            fileStamp.AddStamp(stamp);

            // Save the result
            fileStamp.Save(outputPdf);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPdf}'.");
    }
}
