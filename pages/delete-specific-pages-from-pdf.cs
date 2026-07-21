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
        const string configPath    = "pages_to_delete.txt"; // one page number per line

        // Verify input files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Configuration file not found: {configPath}");
            return;
        }

        // Read page numbers from configuration file (1‑based indexing)
        int[] pagesToDelete;
        try
        {
            pagesToDelete = File.ReadAllLines(configPath)
                                .Select(line => line.Trim())
                                .Where(line => !string.IsNullOrEmpty(line))
                                .Select(int.Parse)
                                .ToArray();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error parsing configuration file: {ex.Message}");
            return;
        }

        // Delete specified pages
        try
        {
            using (Document doc = new Document(inputPdfPath))
            {
                // PageCollection.Delete(int[]) expects 1‑based page numbers
                doc.Pages.Delete(pagesToDelete);
                doc.Save(outputPdfPath);
            }

            Console.WriteLine($"Pages deleted and saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}