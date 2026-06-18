using System;
using System.IO;
using System.Linq;
using Aspose.Pdf; // Aspose.Pdf namespace contains Document and PageCollection

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";
        // Output PDF file path after deletions
        const string outputPdfPath = "output.pdf";
        // Text file containing page numbers to delete (one number per line)
        const string pagesToDeletePath = "pages_to_delete.txt";

        // Validate input files
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(pagesToDeletePath))
        {
            Console.Error.WriteLine($"Page list file not found: {pagesToDeletePath}");
            return;
        }

        try
        {
            // Read page numbers from the text file, ignore empty lines and whitespace
            int[] pagesToDelete = File.ReadAllLines(pagesToDeletePath)
                                      .Select(line => line.Trim())
                                      .Where(line => !string.IsNullOrEmpty(line))
                                      .Select(line => int.Parse(line))
                                      .ToArray();

            // Load the PDF document
            using (Document doc = new Document(inputPdfPath))
            {
                // Delete the specified pages (Page numbers are 1‑based)
                doc.Pages.Delete(pagesToDelete);

                // Save the modified document
                doc.Save(outputPdfPath);
            }

            Console.WriteLine($"Pages deleted and saved to '{outputPdfPath}'.");
        }
        catch (FormatException fe)
        {
            Console.Error.WriteLine($"Invalid page number format: {fe.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}