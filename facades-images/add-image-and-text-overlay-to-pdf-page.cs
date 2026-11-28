using System;
using System.IO;
using System.Drawing; // for System.Drawing.Color
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string imagePath = "overlay.png";   // image to place on page 33

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // PdfFileMend is a Facades class used for low‑level PDF modifications.
        PdfFileMend mend = new PdfFileMend();

        // Bind the existing PDF document.
        mend.BindPdf(inputPdf);

        // Add the image to page 33.
        // Coordinates are defined by a rectangle: lower‑left (250,250) and upper‑right (350,350).
        // Adjust the upper‑right values to control the image size.
        mend.AddImage(imagePath, 33, 250, 250, 350, 350);

        // Add formatted text "Overlay" on the same page.
        // Use Aspose.Pdf.Facades.FormattedText – the constructor requires a System.Drawing.Color,
        // a font name (or FontStyle enum), an EncodingType, a bold flag and a font size (float).
        FormattedText overlayText = new FormattedText(
            "Overlay",                     // text to add
            System.Drawing.Color.Black,    // colour (System.Drawing.Color, not Aspose.Pdf.Color)
            "Helvetica",                  // font name (string overload works)
            EncodingType.Winansi,          // encoding
            false,                         // isBold
            12f);                          // font size

        // Add the text to page 33 within the same rectangle used for the image.
        mend.AddText(overlayText, 33, 250, 250, 350, 350);

        // Save the modified PDF to a new file.
        mend.Save(outputPdf);
        mend.Close(); // optional, releases resources

        Console.WriteLine($"Modified PDF saved to '{outputPdf}'.");
    }
}
