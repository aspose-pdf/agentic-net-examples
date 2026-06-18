using System;
using System.IO;
using Aspose.Pdf.Facades;          // PdfFileStamp, Stamp, FormattedText, EncodingType
using System.Drawing;             // System.Drawing.Color for text colors

class Program
{
    static void Main()
    {
        // Paths for input and output PDFs
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Initialize PdfFileStamp and specify input/output files
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.InputFile  = inputPdf;
        fileStamp.OutputFile = outputPdf;

        // ----- First header line (red) -----
        FormattedText ftLine1 = new FormattedText(
            "First Line",                // text
            Color.Red,                  // text color
            "Helvetica",                // font name
            EncodingType.Winansi,       // encoding
            false,                      // embed font
            24);                        // font size

        Stamp stampLine1 = new Stamp();
        stampLine1.BindLogo(ftLine1);
        // Position near the top of the page (X, Y). Adjust Y for desired top margin.
        stampLine1.SetOrigin(50f, 800f);
        fileStamp.AddStamp(stampLine1);

        // ----- Second header line (blue) -----
        FormattedText ftLine2 = new FormattedText(
            "Second Line",
            Color.Blue,
            "Helvetica",
            EncodingType.Winansi,
            false,
            24);

        Stamp stampLine2 = new Stamp();
        stampLine2.BindLogo(ftLine2);
        // Slightly lower Y to create a new line
        stampLine2.SetOrigin(50f, 770f);
        fileStamp.AddStamp(stampLine2);

        // ----- Third header line (green) -----
        FormattedText ftLine3 = new FormattedText(
            "Third Line",
            Color.Green,
            "Helvetica",
            EncodingType.Winansi,
            false,
            24);

        Stamp stampLine3 = new Stamp();
        stampLine3.BindLogo(ftLine3);
        // Further lower Y for the third line
        stampLine3.SetOrigin(50f, 740f);
        fileStamp.AddStamp(stampLine3);

        // Save the stamped PDF and release resources
        fileStamp.Save(outputPdf);
        fileStamp.Close();

        Console.WriteLine($"Multi‑line header stamp applied. Output saved to '{outputPdf}'.");
    }
}