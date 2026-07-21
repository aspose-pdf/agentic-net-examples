using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "source.pdf";
        const string outputPdf = "booklet_A5.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // PdfFileEditor does NOT implement IDisposable; instantiate directly.
            PdfFileEditor editor = new PdfFileEditor();

            // Use the overload that accepts a custom page size (A5).
            bool success = editor.MakeBooklet(inputPdf, outputPdf, PageSize.A5);

            if (success)
                Console.WriteLine($"Booklet created successfully: {outputPdf}");
            else
                Console.Error.WriteLine("Failed to create booklet.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}