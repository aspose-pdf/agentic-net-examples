using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "edited.pdf";
        // %NUM% will be replaced with the page number (1‑based) for each output file.
        const string outputTemplate = "page%NUM%.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // PdfFileEditor does NOT implement IDisposable and has no Close method.
        PdfFileEditor editor = new PdfFileEditor();

        // Split the PDF into single‑page documents and save them using the template.
        // Each resulting file will contain the original page together with its annotations.
        editor.SplitToPages(inputPdf, outputTemplate);

        // No explicit resource release is required for PdfFileEditor.
        Console.WriteLine("PDF successfully split into individual pages.");
    }
}