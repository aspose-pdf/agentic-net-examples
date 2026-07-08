using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "source.pdf";
        const string outputPdf = "booklet_A5.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // PdfFileEditor does NOT implement IDisposable; instantiate it directly.
        PdfFileEditor editor = new PdfFileEditor();
        // Create a booklet with custom A5 page size using the overload that accepts PageSize.
        bool result = editor.MakeBooklet(inputPdf, outputPdf, PageSize.A5);

        if (result)
            Console.WriteLine($"Booklet created successfully: {outputPdf}");
        else
            Console.Error.WriteLine("Failed to create booklet.");
    }
}