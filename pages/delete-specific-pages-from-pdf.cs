using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string pagesFile = "pages_to_delete.txt";
        const string outputPdf = "output.pdf";

        // Verify input files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(pagesFile))
        {
            Console.Error.WriteLine($"Pages list file not found: {pagesFile}");
            return;
        }

        // Read page numbers (1‑based) from the text file, ignoring empty lines
        int[] pagesToDelete = File.ReadAllLines(pagesFile)
                                   .Select(line => line.Trim())
                                   .Where(line => !string.IsNullOrEmpty(line))
                                   .Select(line => int.Parse(line))
                                   .ToArray();

        if (pagesToDelete.Length == 0)
        {
            Console.WriteLine("No page numbers to delete.");
            return;
        }

        // Open the PDF document with deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Delete the specified pages (Page numbers are 1‑based)
            doc.Pages.Delete(pagesToDelete);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Deleted pages saved to '{outputPdf}'.");
    }
}