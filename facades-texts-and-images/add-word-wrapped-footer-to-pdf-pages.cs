using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;   // for EncodingType

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF via the facade (no need for a Document instance here)
        PdfFileStamp stamp = new PdfFileStamp();
        stamp.BindPdf(inputPdf);

        // Create formatted text for the footer.
        // FormattedText constructor requires System.Drawing.Color for the text color.
        FormattedText footerText = new FormattedText(
            "This is a sample footer that demonstrates word‑by‑word wrapping. " +
            "If the text is longer than the page width, it will wrap automatically " +
            "according to the left and right margins provided.",
            System.Drawing.Color.DarkGray,   // text color
            "Helvetica",                     // font name
            EncodingType.Winansi,            // encoding
            false,                           // embed font (false = use system font)
            9);                              // font size

        // Define margins: bottom margin positions the footer, left/right margins define the wrap width.
        float bottomMargin = 20f;   // distance from the bottom edge of the page
        float leftMargin   = 50f;   // left side margin for the footer text
        float rightMargin  = 50f;   // right side margin for the footer text

        // Add the footer to every page of the document.
        stamp.AddFooter(footerText, bottomMargin, leftMargin, rightMargin);

        // Save the result and release resources.
        stamp.Save(outputPdf);
        stamp.Close();

        Console.WriteLine($"Footer added and saved to '{outputPdf}'.");
    }
}