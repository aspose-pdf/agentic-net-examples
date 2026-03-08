using System;
using System.IO;
using Aspose.Pdf.Facades;   // PdfFileEditor resides here

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf  = "input.pdf";
        // Output PDF file path (N‑up result)
        const string outputPdf = "output_nup.pdf";

        // Define N‑up layout: number of columns (x) and rows (y)
        const int columns = 2;   // e.g., 2 pages per row
        const int rows    = 2;   // e.g., 2 pages per column (total 4 pages per sheet)

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // PdfFileEditor does NOT implement IDisposable, so we do NOT wrap it in a using block.
        // It provides the MakeNUp method which creates the N‑up layout.
        PdfFileEditor editor = new PdfFileEditor();

        // Use the overload that takes file paths and the desired column/row count.
        // This method returns true on success; we can check the result if needed.
        bool success = editor.MakeNUp(inputPdf, outputPdf, columns, rows);

        if (success)
        {
            Console.WriteLine($"N‑up PDF created successfully: {outputPdf}");
        }
        else
        {
            Console.Error.WriteLine("Failed to create N‑up PDF.");
        }

        // No explicit disposal required for PdfFileEditor.
    }
}