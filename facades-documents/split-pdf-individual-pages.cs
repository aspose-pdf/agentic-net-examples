using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputTemplate = "page%NUM%.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfFileEditor does not implement IDisposable, so a simple instance is sufficient.
        PdfFileEditor editor = new PdfFileEditor();
        // Split the PDF into single‑page documents and save them using the template.
        editor.SplitToPages(inputPath, outputTemplate);

        Console.WriteLine("PDF has been split into individual page files.");
    }
}