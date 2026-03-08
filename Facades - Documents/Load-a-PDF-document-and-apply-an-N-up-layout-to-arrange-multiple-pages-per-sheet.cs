using System;
using System.IO;
using Aspose.Pdf.Facades;   // PdfFileEditor
using Aspose.Pdf;          // PageSize enum

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "nup_output.pdf";

        // Number of columns (x) and rows (y) per sheet
        const int columns = 2; // e.g., 2-up horizontally
        const int rows    = 1; // 1 row

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // PdfFileEditor does NOT implement IDisposable; no using block needed
            PdfFileEditor editor = new PdfFileEditor();

            // Create N-up layout; you can also specify a page size, e.g., PageSize.A4
            bool success = editor.MakeNUp(inputPath, outputPath, columns, rows, PageSize.A4);

            if (success)
                Console.WriteLine($"N-up PDF created successfully: {outputPath}");
            else
                Console.Error.WriteLine("Failed to create N-up PDF.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}