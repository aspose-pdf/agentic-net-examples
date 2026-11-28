using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "booklet_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // PdfFileEditor does NOT implement IDisposable, so no using block.
            PdfFileEditor editor = new PdfFileEditor();

            // Create a booklet layout suitable for double‑sided printing.
            // The default BookletPrintMode arranges pages for folding and binding.
            editor.MakeBooklet(inputPath, outputPath);

            Console.WriteLine($"Booklet PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during booklet creation: {ex.Message}");
        }
    }
}