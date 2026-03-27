using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        string inputPath = "input.pdf";
        string outputFolder = "output_pages";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        Directory.CreateDirectory(outputFolder);
        string template = Path.Combine(outputFolder, "page%NUM%.pdf");

        try
        {
            PdfFileEditor editor = new PdfFileEditor();
            editor.SplitToPages(inputPath, template);
            Console.WriteLine($"PDF split into individual pages in folder: {outputFolder}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error splitting PDF: {ex.Message}");
        }
    }
}
