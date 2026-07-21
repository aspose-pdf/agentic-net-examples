using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_2up.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Create an instance of PdfFileEditor (no IDisposable)
        Aspose.Pdf.Facades.PdfFileEditor editor = new Aspose.Pdf.Facades.PdfFileEditor();

        // Make a 2‑up layout: 2 columns (x) and 1 row (y)
        bool result = editor.MakeNUp(inputPdf, outputPdf, 2, 1);

        if (result)
        {
            Console.WriteLine($"2‑up PDF created successfully: {outputPdf}");
        }
        else
        {
            Console.Error.WriteLine("Failed to create 2‑up PDF.");
        }
    }
}