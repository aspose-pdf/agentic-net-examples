using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePdf = "source.pdf";
        const string outputPdf = "booklet.pdf";

        if (!File.Exists(sourcePdf))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdf}");
            return;
        }

        // PdfFileEditor does not implement IDisposable; no using block needed.
        PdfFileEditor editor = new PdfFileEditor();

        // Use the correct MakeBooklet overload (no PageSize argument).
        // This creates a booklet from pages 1‑20 where left pages appear first.
        bool success = editor.MakeBooklet(sourcePdf, outputPdf);
        if (!success)
        {
            Console.Error.WriteLine("Failed to create booklet.");
            return;
        }

        Console.WriteLine($"Booklet created successfully: {outputPdf}");
    }
}