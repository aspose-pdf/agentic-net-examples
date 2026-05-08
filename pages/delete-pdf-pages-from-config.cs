using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string configPath = "pages_to_delete.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Configuration file not found: {configPath}");
            return;
        }

        // Read page numbers (comma, semicolon, or newline separated) and convert to an int array.
        int[] pagesToDelete = File.ReadAllText(configPath)
                                  .Split(new[] { ',', ';', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
                                  .Select(s => int.Parse(s.Trim()))
                                  .ToArray();

        // Open the PDF, delete the specified pages, and save the result.
        using (Document doc = new Document(inputPdf))
        {
            if (pagesToDelete.Length > 0)
            {
                // Page numbers are 1‑based; Delete(int[]) removes all listed pages.
                doc.Pages.Delete(pagesToDelete);
            }

            doc.Save(outputPdf);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPdf}'.");
    }
}