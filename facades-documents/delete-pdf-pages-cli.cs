using System;
using System.IO;
using System.Linq;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments: input PDF path, output PDF path, pages to delete (comma‑separated)
        if (args.Length < 3)
        {
            Console.WriteLine("Usage: DeletePages <input.pdf> <output.pdf> <pages>");
            Console.WriteLine("Example: DeletePages input.pdf output.pdf 2,3,5");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string pagesArgument = args[2];

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        // Parse the page numbers (1‑based) from the comma‑separated string
        int[] pagesToDelete;
        try
        {
            pagesToDelete = pagesArgument
                .Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries)
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
            bool success = editor.Delete(inputPath, pagesToDelete, outputPath);

            if (success)
                Console.WriteLine($"Successfully deleted pages. Output saved to '{outputPath}'.");
            else
                Console.Error.WriteLine("Failed to delete pages (method returned false).");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Operation failed: {ex.Message}");
        }
    }
}