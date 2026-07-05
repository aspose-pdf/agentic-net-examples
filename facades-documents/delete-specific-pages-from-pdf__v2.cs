using System;
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
        if (args.Length < 3)
        {
            Console.WriteLine("Usage: DeletePages <input.pdf> <output.pdf> <page1,page2,...>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string pagesArg = args[2];

        // Parse the page numbers into an int array
        int[] pagesToDelete = pagesArg
            .Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(p => int.Parse(p.Trim()))
            .ToArray();

        // Use Aspose.Pdf.Facades.PdfFileEditor to delete the specified pages
        PdfFileEditor editor = new PdfFileEditor();
        bool result = editor.Delete(inputPath, pagesToDelete, outputPath);

        if (result)
        {
            Console.WriteLine($"Pages deleted successfully. Output saved to '{outputPath}'.");
        }
        else
        {
            Console.WriteLine("Failed to delete pages.");
        }
    }
}