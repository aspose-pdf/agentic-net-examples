using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // for EncodingType

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create formatted text for the footer (text, color, font, encoding, embedFont, fontSize)
        FormattedText footer = new FormattedText(
            "Footer text",                     // text content
            System.Drawing.Color.Gray,         // text color
            "Helvetica",                       // font name
            EncodingType.Winansi,              // encoding
            false,                             // embed font flag
            12);                               // font size

        // Initialize the PdfFileStamp facade and bind the source PDF
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPath);

        // Add footer with a bottom margin of 10 points (exactly 10 points above the page bottom)
        fileStamp.AddFooter(footer, 10);

        // Save the stamped PDF and release resources
        fileStamp.Save(outputPath);
        fileStamp.Close();
    }
}