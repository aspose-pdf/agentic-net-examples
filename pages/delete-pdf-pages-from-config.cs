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
            Console.Error.WriteLine($"Config file not found: {configPath}");
            return;
        }

        // Read page numbers from the configuration file.
        // The file may contain numbers separated by commas, semicolons, or new lines.
        int[] pagesToDelete;
        try
        {
            string configContent = File.ReadAllText(configPath);
            pagesToDelete = configContent
                .Split(new[] { ',', ';', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => int.Parse(s.Trim()))
                .ToArray();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to parse page numbers: {ex.Message}");
            return;
        }

        if (pagesToDelete.Length == 0)
        {
            Console.WriteLine("No pages specified for deletion.");
            return;
        }

        try
        {
            // Load the PDF, delete the specified pages, and save the result.
            using (Document doc = new Document(inputPdf))
            {
                // Delete pages using the overload that accepts an int array.
                // Page numbers are 1‑based as required by Aspose.Pdf.
                doc.Pages.Delete(pagesToDelete);
                doc.Save(outputPdf);
            }

            Console.WriteLine($"Pages deleted and saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}