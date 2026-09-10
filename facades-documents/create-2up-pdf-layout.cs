using System;
using System.IO;
using Aspose.Pdf.Facades; // Contains PdfFileEditor

class Program
{
    static void Main()
    {
        // Path to the source PDF
        const string inputPath = "input.pdf";
        // Desired name for the 2‑up output PDF
        const string outputPath = "output_2up.pdf";

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create an instance of PdfFileEditor (no IDisposable implementation)
        PdfFileEditor pdfEditor = new PdfFileEditor();

        // Make a 2‑up layout:
        // x = 2 columns, y = 1 row (two pages per output page, placed horizontally)
        bool result = pdfEditor.MakeNUp(inputPath, outputPath, 2, 1);

        if (result)
        {
            Console.WriteLine($"2‑up PDF created successfully: {outputPath}");
        }
        else
        {
            Console.Error.WriteLine("Failed to create 2‑up PDF.");
        }
    }
}