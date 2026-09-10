using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string configPath    = "pages_to_delete.txt"; // one page number per line or comma‑separated

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Config file not found: {configPath}");
            return;
        }

        // Read page numbers from the config file and convert to an int[].
        // Accept both line‑separated and comma‑separated formats.
        int[] pagesToDelete = File.ReadAllText(configPath)
                                  .Split(new[] { ',', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
                                  .Select(s => int.Parse(s.Trim()))
                                  .ToArray();

        if (pagesToDelete.Length == 0)
        {
            Console.WriteLine("No pages specified for deletion.");
            return;
        }

        // Load the PDF, delete the specified pages, and save the result.
        using (Document doc = new Document(inputPdfPath))
        {
            // Page numbers are 1‑based; Delete(int[]) expects the same.
            doc.Pages.Delete(pagesToDelete);
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Deleted pages [{string.Join(", ", pagesToDelete)}] and saved to '{outputPdfPath}'.");
    }
}