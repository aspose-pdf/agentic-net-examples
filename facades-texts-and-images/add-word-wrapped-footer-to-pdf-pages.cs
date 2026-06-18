using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;   // FormattedText resides here

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

        // Create the formatted text that will appear in the footer.
        // Constructor parameters: text, text color (System.Drawing.Color), font name,
        // encoding, embed font flag, font size.
        FormattedText footer = new FormattedText(
            "This is a sample footer that will wrap word by word across the page width.",
            System.Drawing.Color.Gray,
            "Helvetica",
            EncodingType.Winansi,
            false,
            10);   // font size 10 points

        // PdfFileStamp adds headers/footers to every page of the document.
        // The second argument is the bottom margin (distance from the page bottom).
        PdfFileStamp fileStamp = new PdfFileStamp(inputPdf, outputPdf);
        fileStamp.AddFooter(footer, 20);   // 20 points bottom margin
        fileStamp.Close();                 // Persist changes and release resources

        Console.WriteLine($"Footer added successfully. Output saved to '{outputPdf}'.");
    }
}