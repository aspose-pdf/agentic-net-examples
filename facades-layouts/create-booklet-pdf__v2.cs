using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "booklet.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // PdfFileEditor does not implement IDisposable; instantiate directly.
            PdfFileEditor editor = new PdfFileEditor();

            // Create a booklet layout from the source PDF and save it.
            editor.MakeBooklet(inputPath, outputPath);

            Console.WriteLine($"Booklet PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error creating booklet: {ex.Message}");
        }
    }
}