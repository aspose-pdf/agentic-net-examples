using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create a PdfFileEditor instance (no IDisposable implementation)
        PdfFileEditor editor = new PdfFileEditor();

        // Add 10% margins on all pages.
        // Passing null for the pages array processes every page in the document.
        bool success = editor.AddMarginsPct(
            source: inputPath,
            destination: outputPath,
            pages: null,
            leftMargin: 10,   // 10% left margin
            rightMargin: 10,  // 10% right margin
            topMargin: 10,    // 10% top margin
            bottomMargin: 10  // 10% bottom margin
        );

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