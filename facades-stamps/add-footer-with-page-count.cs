using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

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

        // Initialize the PdfFileStamp facade
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.InputFile = inputPath;
        fileStamp.OutputFile = outputPath;

        // Create formatted text with the page count placeholder
        FormattedText footer = new FormattedText("Page {page_count}");

        // Add the footer to each page with a bottom margin of 20 points
        fileStamp.AddFooter(footer, 20);

        // Save changes and release resources
        fileStamp.Close();

        Console.WriteLine($"Footer with page count added to '{outputPath}'.");
    }
}