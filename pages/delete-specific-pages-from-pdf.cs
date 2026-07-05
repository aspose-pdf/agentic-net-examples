using System;
using System.IO;
using System.Linq;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string configPath    = "pages_to_delete.txt"; // one page number per line or comma‑separated

        // Validate files
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

        // Read page numbers from the configuration file.
        // Supports lines like "3", " 5 ", or "1,2,4".
        int[] pagesToDelete = File.ReadAllLines(configPath)
            .SelectMany(line => line.Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries))
            .Select(token => token.Trim())
            .Where(token => !string.IsNullOrEmpty(token))
            .Select(token =>
            {
                if (int.TryParse(token, out int n))
                    return n;
                Console.Error.WriteLine($"Invalid page number '{token}' – ignored.");
                return -1; // placeholder for invalid entries
            })
            .Where(n => n > 0) // keep only valid positive numbers
            .Distinct()
            .OrderBy(n => n)   // ordering is optional; Delete(int[]) does not depend on order
            .ToArray();

        if (pagesToDelete.Length == 0)
        {
            Console.WriteLine("No valid page numbers to delete – nothing to do.");
            return;
        }

        // Load the PDF, delete the specified pages, and save the result.
        // Document implements IDisposable – use a using block as per the lifecycle rule.
        using (Document doc = new Document(inputPdfPath))
        {
            // Page numbers are 1‑based; Delete(int[]) expects an array of such numbers.
            doc.Pages.Delete(pagesToDelete);

            // Save the modified document.
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Deleted pages [{string.Join(", ", pagesToDelete)}] and saved to '{outputPdfPath}'.");
    }
}