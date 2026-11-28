using System;
using System.IO;
using Aspose.Pdf;
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

        // Load the PDF with PdfFileStamp and add a footer
        using (PdfFileStamp stamp = new PdfFileStamp())
        {
            // Bind the source PDF file
            stamp.BindPdf(inputPath);

            // Create formatted text for the footer. Use fully‑qualified System.Drawing.Color to avoid
            // ambiguity and the FontStyle enum (not a string) for the font.
            var footer = new FormattedText(
                "Confidential Footer",          // text
                System.Drawing.Color.Gray,       // text colour (System.Drawing.Color)
                FontStyle.Helvetica,            // font style enum from Aspose.Pdf.Facades
                EncodingType.Winansi,            // encoding enum
                false,                           // embed the font?
                12f);                            // font size

            // Add the footer to each page with a bottom margin of 10 points
            stamp.AddFooter(footer, 10f);

            // Save the modified PDF
            stamp.Save(outputPath);
        }

        Console.WriteLine($"Footer added and saved to '{outputPath}'.");
    }
}
