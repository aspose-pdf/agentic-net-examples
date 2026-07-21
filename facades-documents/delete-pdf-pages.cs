using System;
using System.IO;
using System.Linq;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments: <inputPdf> <outputPdf> <pagesToDelete>
        // pagesToDelete should be a comma‑separated list of 1‑based page numbers, e.g. "2,3,5"
        if (args.Length != 3)
        {
            Console.Error.WriteLine("Usage: DeletePages <inputPdf> <outputPdf> <pagesToDelete>");
            Console.Error.WriteLine("Example: DeletePages input.pdf output.pdf 2,3,5");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string pagesArg = args[2];

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        // Parse the page numbers
        int[] pagesToDelete;
        try
        {
            pagesToDelete = pagesArg
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(p => int.Parse(p.Trim()))
                .ToArray();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: Invalid page list – {ex.Message}");
            return;
        }

        try
        {
            // PdfFileEditor handles loading and saving internally; no using block needed
            PdfFileEditor editor = new PdfFileEditor();

            bool success = editor.Delete(inputPath, pagesToDelete, outputPath);

            if (success)
                Console.WriteLine($"Pages deleted successfully. Output saved to '{outputPath}'.");
            else
                Console.Error.WriteLine("Failed to delete pages. Check the input file and page numbers.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}