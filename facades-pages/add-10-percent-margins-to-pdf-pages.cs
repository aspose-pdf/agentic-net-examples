using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPath  = "input.pdf";
        const string outputPath = "output_resized.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // Create the PdfFileEditor facade
        PdfFileEditor fileEditor = new PdfFileEditor();

        // Add 10% margins on all sides for all pages (pages = null)
        // left, right, top, bottom margins are specified in percents of the original page size
        bool success = fileEditor.AddMarginsPct(
            source:      inputPath,
            destination: outputPath,
            pages:       null,   // null processes every page in the document
            leftMargin:  10,     // 10% left margin
            rightMargin: 10,     // 10% right margin
            topMargin:   10,     // 10% top margin
            bottomMargin:10);    // 10% bottom margin

        // Report the result
        if (success)
        {
            Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
        }
        else
        {
            Console.Error.WriteLine("Failed to resize PDF contents.");
        }
    }
}