using System;
using System.IO;
using System.Drawing;
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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create a PdfFileStamp instance (does NOT implement IDisposable)
        var stamp = new PdfFileStamp();

        // Bind the existing PDF file to the stamp object
        stamp.BindPdf(inputPath);

        // Build the formatted footer text using the Facades.FormattedText constructor.
        // Text can contain line breaks ("\n") or you can use AddNewLineText() after construction.
        var footer = new FormattedText(
            "Prepared by\nJohn Doe",
            Color.Black,
            "Helvetica",
            EncodingType.Winansi,
            false,
            12);

        // Add the footer with a bottom margin of 15 points
        stamp.AddFooter(footer, 15f);

        // Save the modified PDF to a new file
        stamp.Save(outputPath);

        Console.WriteLine($"Footer added and saved to '{outputPath}'.");
    }
}
