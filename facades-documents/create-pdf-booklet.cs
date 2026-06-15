using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "booklet.pdf";

        // Verify the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfFileEditor does not implement IDisposable, so a simple instance is sufficient.
        PdfFileEditor editor = new PdfFileEditor();

        // Create a booklet using default settings.
        bool result = editor.MakeBooklet(inputPath, outputPath);

        if (result)
        {
            Console.WriteLine($"Booklet successfully created at '{outputPath}'.");
        }
        else
        {
            Console.Error.WriteLine("Failed to create booklet.");
        }
    }
}