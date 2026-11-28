using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "nup_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfFileEditor does NOT implement IDisposable; instantiate directly.
        PdfFileEditor editor = new PdfFileEditor();

        // Define a 2‑by‑3 N‑up layout (2 rows, 3 columns).
        int rows    = 2;
        int columns = 3;

        try
        {
            editor.MakeNUp(inputPath, outputPath, rows, columns);
            Console.WriteLine($"N‑up PDF created at '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error creating N‑up PDF: {ex.Message}");
        }
    }
}