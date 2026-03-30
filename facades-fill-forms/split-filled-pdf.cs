using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "filled.pdf";
        const string outputTemplate = "page%NUM%.pdf"; // %NUM% will be replaced by page number

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            Aspose.Pdf.Facades.PdfFileEditor pdfEditor = new Aspose.Pdf.Facades.PdfFileEditor();
            pdfEditor.SplitToPages(inputPath, outputTemplate);
            Console.WriteLine("PDF successfully split into single‑page files.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}