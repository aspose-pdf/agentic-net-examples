using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file (must exist)
        const string inputPath = "input.pdf";
        // Output PDF file with 2‑up layout
        const string outputPath = "output_2up.pdf";

        // Verify the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create the PdfFileEditor facade (does not implement IDisposable)
        PdfFileEditor editor = new PdfFileEditor();

        // Create a 2‑up layout: 2 columns, 1 row per sheet
        // This method returns true on success, false otherwise
        bool success = editor.MakeNUp(inputPath, outputPath, 2, 1);

        if (success)
        {
            Console.WriteLine($"2‑up PDF created successfully: {outputPath}");
        }
        else
        {
            Console.Error.WriteLine("Failed to create 2‑up PDF layout.");
        }
    }
}