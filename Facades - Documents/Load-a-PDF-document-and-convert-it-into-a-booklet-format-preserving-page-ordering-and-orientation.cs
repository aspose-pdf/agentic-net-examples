using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "booklet_output.pdf";

        // Verify that the source PDF exists before processing.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfFileEditor is a facade class; it does not implement IDisposable,
        // so it is instantiated without a using block.
        PdfFileEditor editor = new PdfFileEditor();

        // Convert the input PDF into a booklet format.
        // The MakeBooklet method reorders pages and adjusts orientation as needed.
        bool success = editor.MakeBooklet(inputPath, outputPath);

        if (success)
        {
            Console.WriteLine($"Booklet created successfully: {outputPath}");
        }
        else
        {
            Console.Error.WriteLine("Failed to create booklet.");
        }
    }
}