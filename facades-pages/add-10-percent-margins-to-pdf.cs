using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source PDF and the resized output PDF
        const string inputPath = "input.pdf";
        const string outputPath = "output_resized.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // Create the PdfFileEditor facade
        var fileEditor = new PdfFileEditor();

        // Add 10% margins on all sides for all pages.
        // The AddMarginsPct overload expects an int[] for the pages argument.
        // Passing null processes every page.
        bool success = fileEditor.AddMarginsPct(
            source: inputPath,
            destination: outputPath,
            pages: null,          // all pages
            leftMargin: 10,
            rightMargin: 10,
            topMargin: 10,
            bottomMargin: 10);

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
