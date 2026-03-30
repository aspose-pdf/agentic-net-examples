using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputTemplate = "page%NUM%.pdf"; // %NUM% will be replaced by page number

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }

        try
        {
            PdfFileEditor pdfEditor = new PdfFileEditor();
            pdfEditor.SplitToPages(inputPath, outputTemplate);
            Console.WriteLine("PDF successfully split into single‑page files using template: " + outputTemplate);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("Error during split: " + ex.Message);
        }
    }
}