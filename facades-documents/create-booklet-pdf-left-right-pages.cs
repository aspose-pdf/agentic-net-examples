using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "booklet.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Define left‑hand (odd) and right‑hand (even) page sequences.
        // Example for an 8‑page document:
        //   Left pages  : 2, 4, 6, 8
        //   Right pages : 1, 3, 5, 7
        int[] leftPages  = new int[] { 2, 4, 6, 8 };
        int[] rightPages = new int[] { 1, 3, 5, 7 };

        // Create the PdfFileEditor facade and generate the booklet.
        PdfFileEditor editor = new PdfFileEditor();
        bool success = editor.MakeBooklet(inputPdf, outputPdf, leftPages, rightPages);

        if (success)
            Console.WriteLine($"Booklet created successfully: {outputPdf}");
        else
            Console.Error.WriteLine("Failed to create booklet.");
    }
}