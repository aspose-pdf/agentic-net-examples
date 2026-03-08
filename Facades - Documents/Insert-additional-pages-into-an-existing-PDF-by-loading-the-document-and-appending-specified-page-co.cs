using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the original PDF, the PDF containing pages to insert, and the result PDF.
        const string sourcePdf = "source.pdf";
        const string insertPdf = "insert.pdf";
        const string outputPdf = "output.pdf";

        // Verify that the input files exist.
        if (!File.Exists(sourcePdf))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdf}");
            return;
        }

        if (!File.Exists(insertPdf))
        {
            Console.Error.WriteLine($"Insert file not found: {insertPdf}");
            return;
        }

        // PdfFileEditor does NOT implement IDisposable, so we instantiate it directly.
        PdfFileEditor editor = new PdfFileEditor();

        // Insert pages 2 through 4 from insertPdf after page 1 of sourcePdf.
        // Parameters: inputFile, insertLocation, portFile, startPage, endPage, outputFile
        bool result = editor.Insert(sourcePdf, 1, insertPdf, 2, 4, outputPdf);

        Console.WriteLine(result
            ? $"Pages inserted successfully. Output saved to '{outputPdf}'."
            : "Failed to insert pages.");
    }
}