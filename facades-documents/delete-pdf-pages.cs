using System;
using System.IO;
using System.Linq;
using Aspose.Pdf.Facades;

class DeletePdfPages
{
    static void Main(string[] args)
    {
        // Expected arguments:
        // args[0] - input PDF file path
        // args[1] - output PDF file path
        // args[2] - comma‑separated list of page numbers to delete (1‑based)
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: DeletePdfPages <input.pdf> <output.pdf> <pages-to-delete>");
            Console.Error.WriteLine("Example: DeletePdfPages input.pdf output.pdf 2,3,5");
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

        // Parse the page numbers (1‑based) into an int array
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
            Console.Error.WriteLine($"Error parsing page numbers: {ex.Message}");
            return;
        }

        try
        {
            // Use PdfFileEditor to delete the specified pages
            PdfFileEditor editor = new PdfFileEditor();
            bool success = editor.Delete(inputPath, pagesToDelete, outputPath);

            if (success)
                Console.WriteLine($"Pages deleted successfully. Output saved to '{outputPath}'.");
            else
                Console.Error.WriteLine("Failed to delete pages. Operation returned false.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Exception during processing: {ex.Message}");
        }
    }
}