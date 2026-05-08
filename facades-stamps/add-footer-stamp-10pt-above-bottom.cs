using System;
using System.IO;
using Aspose.Pdf.Facades;   // PdfFileStamp and FormattedText are defined here

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create a PdfFileStamp facade with input and output files
        PdfFileStamp fileStamp = new PdfFileStamp(inputPath, outputPath);

        // Prepare the footer text (default font/color are used)
        FormattedText footerText = new FormattedText("Footer placed 10 pt above bottom edge");

        // Add the footer with a bottom margin of 10 points.
        // Left and right margins are set to 0 (centered horizontally).
        fileStamp.AddFooter(footerText, 10f, 0f, 0f);

        // Close the facade to write the result to the output file
        fileStamp.Close();

        Console.WriteLine($"Footer stamp added. Output saved to '{outputPath}'.");
    }
}