using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string editedPdfPath = "edited.pdf";
        const string outputTemplate = "output/page%NUM%.pdf";

        if (!File.Exists(editedPdfPath))
        {
            Console.Error.WriteLine($"File not found: {editedPdfPath}");
            return;
        }

        // PdfFileEditor does not implement IDisposable, so we instantiate it directly.
        PdfFileEditor editor = new PdfFileEditor();

        // Split the PDF into single‑page documents.
        // The template must contain %NUM% which will be replaced by the page number (1‑based).
        editor.SplitToPages(editedPdfPath, outputTemplate);

        Console.WriteLine("PDF split into individual pages successfully.");
    }
}