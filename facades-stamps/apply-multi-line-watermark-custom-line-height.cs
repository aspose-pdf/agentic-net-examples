using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "watermarked.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Create the formatted text that will be used as the watermark.
        // All styling (text, colour, font, size, etc.) must be supplied via the constructor.
        FormattedText formatted = new FormattedText(
            "CONFIDENTIAL\nDO NOT DISTRIBUTE\nFOR INTERNAL USE ONLY",
            Color.FromArgb(150, 200, 200, 200), // semi‑transparent gray
            "Helvetica",
            EncodingType.Winansi,
            false,
            48); // base font size

        // If you need extra spacing between the lines you can insert an empty line
        // or a line that contains only spaces. Here we add a blank line after each
        // real line to double the line height.
        formatted.AddNewLineText(" "); // first blank line
        formatted.AddNewLineText("CONFIDENTIAL");
        formatted.AddNewLineText(" ");
        formatted.AddNewLineText("DO NOT DISTRIBUTE");
        formatted.AddNewLineText(" ");
        formatted.AddNewLineText("FOR INTERNAL USE ONLY");

        // Create a Stamp (facade) and bind the formatted text to it.
        Stamp stamp = new Stamp();
        stamp.BindLogo(formatted);
        stamp.IsBackground = true;   // place behind page content
        stamp.Opacity = 0.5f;        // overall opacity
        // Position the stamp at the centre of each page. We will set the origin
        // after we know the page dimensions (here we simply use (0,0) and rely on
        // the default centre alignment of the stamp when added to the document).
        stamp.SetOrigin(0, 0);

        // Apply the stamp to every page of the source PDF.
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPdf);
        fileStamp.AddStamp(stamp);
        fileStamp.Save(outputPdf);
        fileStamp.Close();

        Console.WriteLine($"Watermarked PDF saved to '{outputPdf}'.");
    }
}
