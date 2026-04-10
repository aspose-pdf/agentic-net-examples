using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";
        const string outputPdf  = "output.pdf";
        const string configFile = "pages_to_delete.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(configFile))
        {
            Console.Error.WriteLine($"Configuration file not found: {configFile}");
            return;
        }

        // Read page numbers (one per line or comma‑separated) and convert to an int[].
        // PageCollection.Delete expects 1‑based page numbers.
        int[] pagesToDelete = File.ReadAllText(configFile)
            .Split(new[] { ',', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(s => int.Parse(s.Trim()))
            .ToArray();

        // Load the PDF, delete the specified pages, and save the result.
        using (Document doc = new Document(inputPdf))
        {
            doc.Pages.Delete(pagesToDelete);
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Pages deleted and saved to '{outputPdf}'.");
    }
}