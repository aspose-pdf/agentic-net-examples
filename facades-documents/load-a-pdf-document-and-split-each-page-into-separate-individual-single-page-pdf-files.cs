using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        // Template must contain %NUM% which will be replaced by the page number.
        const string outputTemplate = "output_page%NUM%.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // PdfFileEditor does NOT implement IDisposable, so no using block is needed.
            PdfFileEditor editor = new PdfFileEditor();

            // Split the PDF into single‑page files. Each file will be saved according to the template.
            editor.SplitToPages(inputPdf, outputTemplate);

            Console.WriteLine("PDF successfully split into individual pages.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during splitting: {ex.Message}");
        }
    }
}