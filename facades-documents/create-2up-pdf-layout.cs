using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source PDF and the resulting 2‑up PDF
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_2up.pdf";

        // Verify that the input file exists
        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Create the PdfFileEditor (does not implement IDisposable, so no using block needed)
        PdfFileEditor editor = new PdfFileEditor();

        // Make a 2‑up layout: 2 columns (x) and 1 row (y) – pages are placed side‑by‑side horizontally
        bool success = editor.MakeNUp(inputPdf, outputPdf, 2, 1);

        if (success)
        {
            Console.WriteLine($"2‑up PDF created successfully: {outputPdf}");
        }
        else
        {
            Console.Error.WriteLine("Failed to create 2‑up PDF.");
        }
    }
}