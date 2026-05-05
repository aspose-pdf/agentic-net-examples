using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the edited PDF that contains annotations
        const string inputPdf = "edited.pdf";

        // Template for the output files.
        // %NUM% will be replaced with the page number (1‑based).
        const string outputTemplate = "output/page%NUM%.pdf";

        // Ensure the input file exists
        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // PdfFileEditor does NOT implement IDisposable, so we instantiate it directly.
        PdfFileEditor editor = new PdfFileEditor();

        // Split the PDF into single‑page documents.
        // Each page (including its annotations) is saved to a separate file
        // according to the outputTemplate.
        editor.SplitToPages(inputPdf, outputTemplate);

        Console.WriteLine("PDF split into individual pages successfully.");
    }
}