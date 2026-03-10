using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "nup_output.pdf";
        const int columns = 2; // number of columns (x)
        const int rows = 2;    // number of rows (y)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfFileEditor does not implement IDisposable, so we instantiate it directly.
        PdfFileEditor editor = new PdfFileEditor();

        // Create an N-up PDF: each output page will contain columns x rows pages from the source.
        bool result = editor.MakeNUp(inputPath, outputPath, columns, rows);

        if (result)
            Console.WriteLine($"N-up PDF created successfully at '{outputPath}'.");
        else
            Console.Error.WriteLine("Failed to create N-up PDF.");
    }
}