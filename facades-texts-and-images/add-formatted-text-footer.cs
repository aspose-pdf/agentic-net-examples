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

        // Create formatted text for the footer. The constructor requires System.Drawing.Color.
        FormattedText footerText = new FormattedText(
            "This is a sample footer text that will wrap word by word across the page width.",
            System.Drawing.Color.Gray,
            "Helvetica",
            EncodingType.Winansi,
            false,
            10);

        // Initialize PdfFileStamp with input and output files.
        PdfFileStamp fileStamp = new PdfFileStamp(inputPath, outputPath);
        // Add the footer with a bottom margin of 20 points.
        fileStamp.AddFooter(footerText, 20f);
        fileStamp.Close();

        Console.WriteLine($"Footer added and saved to '{outputPath}'.");
    }
}