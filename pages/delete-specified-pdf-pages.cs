using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF, text file with page numbers (one per line), and output PDF paths
        const string inputPdf   = "input.pdf";
        const string pagesFile  = "pages_to_delete.txt";
        const string outputPdf  = "output.pdf";

        // Validate existence of required files
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(pagesFile))
        {
            Console.Error.WriteLine($"Page list file not found: {pagesFile}");
            return;
        }

        // Read page numbers from the text file, ignoring empty lines and whitespace
        int[] pagesToDelete;
        try
        {
            pagesToDelete = File.ReadAllLines(pagesFile)
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Select(line => int.Parse(line.Trim()))
                .ToArray();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error parsing page numbers: {ex.Message}");
            return;
        }

        // If no pages were specified, nothing to do
        if (pagesToDelete.Length == 0)
        {
            Console.WriteLine("No page numbers provided for deletion.");
            return;
        }

        // Load the PDF, delete the specified pages, and save the result
        using (Document doc = new Document(inputPdf))
        {
            // Page numbers are 1‑based; Delete(int[]) removes all listed pages
            doc.Pages.Delete(pagesToDelete);
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Deleted pages saved to '{outputPdf}'.");
    }
}