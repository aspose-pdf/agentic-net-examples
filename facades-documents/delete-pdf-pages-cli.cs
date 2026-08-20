using System;
using System.IO;
using System.Linq;
using Aspose.Pdf.Facades;

class Program
{
    // Entry point of the command‑line tool.
    // Arguments:
    //   args[0] - path to the input PDF file
    //   args[1] - comma‑separated list of page numbers to delete (1‑based)
    //   args[2] - path for the output PDF file
    static void Main(string[] args)
    {
        if (args.Length != 3)
        {
            Console.Error.WriteLine("Usage: DeletePages <input.pdf> <pages> <output.pdf>");
            Console.Error.WriteLine("  <pages> – comma separated list of page numbers to delete (e.g. 2,3,5)");
            return;
        }

        string inputPath  = args[0];
        string pagesArg   = args[1];
        string outputPath = args[2];

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        // Parse the page numbers; ignore empty entries and invalid numbers.
        int[] pagesToDelete = pagesArg
            .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(p =>
            {
                if (int.TryParse(p.Trim(), out int n) && n > 0)
                    return n;
                else
                    return -1; // sentinel for invalid entry
            })
            .Where(n => n > 0)
            .ToArray();

        if (pagesToDelete.Length == 0)
        {
            Console.Error.WriteLine("Error: No valid page numbers supplied.");
            return;
        }

        // PdfFileEditor does not implement IDisposable, so we instantiate it directly.
        PdfFileEditor editor = new PdfFileEditor();

        try
        {
            bool result = editor.TryDelete(inputPath, pagesToDelete, outputPath);
            if (result)
                Console.WriteLine($"Pages deleted successfully. Output saved to '{outputPath}'.");
            else
                Console.Error.WriteLine("Failed to delete pages. The operation returned false.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Exception during deletion: {ex.Message}");
        }
    }
}