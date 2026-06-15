using System;
using System.IO;
using System.Linq;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments:
        // 0 - input PDF file path
        // 1 - output PDF file path
        // 2 - comma‑separated list of page numbers to delete (1‑based)
        if (args.Length != 3)
        {
            Console.Error.WriteLine("Usage: DeletePages <input.pdf> <output.pdf> <pages>");
            Console.Error.WriteLine("Example: DeletePages input.pdf output.pdf 2,3,5");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string pagesArg = args[2];

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        // Parse the page numbers into an int array
        int[] pages;
        try
        {
            pages = pagesArg
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(p => int.Parse(p.Trim()))
                .ToArray();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error parsing page numbers: {ex.Message}");
            return;
        }

        try
        {
            // Use Aspose.Pdf.Facades.PdfFileEditor to delete the specified pages
            PdfFileEditor editor = new PdfFileEditor();
            bool result = editor.Delete(inputPath, pages, outputPath);

            if (result)
                Console.WriteLine($"Pages deleted successfully. Output saved to '{outputPath}'.");
            else
                Console.Error.WriteLine("Failed to delete pages.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Exception: {ex.Message}");
        }
    }
}