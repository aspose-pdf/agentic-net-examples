using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Drawing; // Required for System.Drawing.Color

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string logoPath = "logo.png";
        const string customText = "Confidential";

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

        // Bind the source PDF
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPdf);

        // Create a stamp that will contain both an image and text
        // Fully qualify the Stamp type to avoid ambiguity between Aspose.Pdf.Stamp and Aspose.Pdf.Facades.Stamp
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();

        // ----- Image part -----
        stamp.BindImage(logoPath);               // Set the logo image
        stamp.SetImageSize(50, 50);              // Width & height (points)
        stamp.SetOrigin(100, 700);               // Position on the page (X, Y)

        // ----- Text part -----
        // Create FormattedText with the desired content, color, font, encoding, embed flag and size
        FormattedText formattedText = new FormattedText(
            customText,                         // Text to display
            System.Drawing.Color.Black,        // Text color (System.Drawing.Color)
            "Helvetica",                      // Font name
            EncodingType.Winansi,
            false,                             // Embedded flag
            24f);                              // Font size (float)

        // Bind the formatted text to the same stamp (this is the correct way; there is no 'Text' property)
        stamp.BindLogo(formattedText);

        // Add the combined stamp to the document (applies to all pages)
        fileStamp.AddStamp(stamp);

        // Save the stamped PDF
        fileStamp.Save(outputPdf);
        fileStamp.Close();

        Console.WriteLine($"Stamped PDF saved to '{outputPdf}'.");
    }
}
