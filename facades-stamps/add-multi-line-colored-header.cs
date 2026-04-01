using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Drawing;
using Aspose.Pdf.Text;

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

        // Initialize the PdfFileStamp facade and bind the source PDF
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPath);

        // First header line – red color
        FormattedText headerLine1 = new FormattedText(
            "First Header Line",
            System.Drawing.Color.Red,
            "Helvetica",
            EncodingType.Winansi,
            false,
            24);
        fileStamp.AddHeader(headerLine1, 20f);

        // Second header line – green color
        FormattedText headerLine2 = new FormattedText(
            "Second Header Line",
            System.Drawing.Color.Green,
            "Helvetica",
            EncodingType.Winansi,
            false,
            24);
        fileStamp.AddHeader(headerLine2, 50f);

        // Third header line – blue color
        FormattedText headerLine3 = new FormattedText(
            "Third Header Line",
            System.Drawing.Color.Blue,
            "Helvetica",
            EncodingType.Winansi,
            false,
            24);
        fileStamp.AddHeader(headerLine3, 80f);

        // Save the stamped PDF and release resources
        fileStamp.Save(outputPath);
        fileStamp.Close();

        Console.WriteLine($"Multi-line colored header added to '{outputPath}'.");
    }
}