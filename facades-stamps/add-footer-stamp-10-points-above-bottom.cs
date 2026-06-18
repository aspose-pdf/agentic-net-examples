using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class FooterStampExample
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // source PDF
        const string outputPdf = "output.pdf";  // result PDF with footer

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Create a FormattedText object that defines the footer content.
        // The constructor parameters are:
        //   text, text color (System.Drawing.Color), font name, encoding, embed flag, font size.
        FormattedText footerText = new FormattedText(
            "Confidential – Footer",          // text to display
            System.Drawing.Color.Gray,        // text color
            "Helvetica",                      // font family
            EncodingType.Winansi,             // encoding
            false,                            // embed font?
            12);                              // font size

        // Use PdfFileStamp (facade) to add the footer.
        // The parameterless constructor is used; the input PDF is bound via BindPdf().
        using (PdfFileStamp fileStamp = new PdfFileStamp())
        {
            // Bind the source PDF file.
            fileStamp.BindPdf(inputPdf);

            // Add the footer 10 points above the bottom edge of each page.
            // The second argument is the bottom margin (distance from the bottom edge).
            fileStamp.AddFooter(footerText, 10f);

            // Save the modified document to the output path.
            fileStamp.Save(outputPdf);
        }

        Console.WriteLine($"Footer added; output saved to '{outputPdf}'.");
    }
}