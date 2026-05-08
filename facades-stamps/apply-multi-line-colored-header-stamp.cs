using System;
using System.IO;
using System.Drawing;               // System.Drawing.Color is required for FormattedText
using Aspose.Pdf.Facades;          // PdfFileStamp, FormattedText, EncodingType

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

        // Initialize the PdfFileStamp facade and specify input/output files
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.InputFile  = inputPdf;
        fileStamp.OutputFile = outputPdf;

        // Create three header lines, each with a distinct font color
        FormattedText headerLine1 = new FormattedText(
            "First Header Line",          // text
            Color.Blue,                  // System.Drawing.Color
            "Helvetica",                 // font name
            EncodingType.Winansi,        // encoding
            false,                       // embed font?
            24);                         // font size

        FormattedText headerLine2 = new FormattedText(
            "Second Header Line",
            Color.Green,
            "Helvetica",
            EncodingType.Winansi,
            false,
            20);

        FormattedText headerLine3 = new FormattedText(
            "Third Header Line",
            Color.Red,
            "Helvetica",
            EncodingType.Winansi,
            false,
            18);

        // Add each line as a separate header; increase top margin for each subsequent line
        float topMargin = 20f;   // distance from the top edge for the first line
        fileStamp.AddHeader(headerLine1, topMargin);
        topMargin += 30f;        // adjust for next line (approximate line height)
        fileStamp.AddHeader(headerLine2, topMargin);
        topMargin += 30f;
        fileStamp.AddHeader(headerLine3, topMargin);

        // Persist changes and release resources
        fileStamp.Close();

        Console.WriteLine($"Multi‑line header stamp applied and saved to '{outputPdf}'.");
    }
}