using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";
        // Output PDF file path (result after deletions)
        const string outputPdfPath = "output.pdf";
        // Text file containing page numbers to delete (one number per line)
        const string pagesToDeletePath = "pages_to_delete.txt";

        // Validate input files
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(pagesToDeletePath))
        {
            Console.Error.WriteLine($"Error: Page list file not found: {pagesToDeletePath}");
            return;
        }

        // Read page numbers from the text file, ignoring empty lines and whitespace
        int[] pagesToDelete;
        try
        {
            pagesToDelete = File.ReadAllLines(pagesToDeletePath)
                                .Select(line => line.Trim())
                                .Where(line => !string.IsNullOrEmpty(line))
                                .Select(line => int.Parse(line))
                                .ToArray();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error parsing page numbers: {ex.Message}");
            return;
        }

        // Ensure there is at least one page number to delete
        if (pagesToDelete.Length == 0)
        {
            Console.WriteLine("No page numbers specified for deletion. Exiting.");
            return;
        }

        // Load the PDF, delete the specified pages, and save the result
        try
        {
            // Document disposal with using (ensures resources are released)
            using (Document doc = new Document(inputPdfPath))
            {
                // Delete pages – PageCollection.Delete(int[]) expects 1‑based page numbers
                doc.Pages.Delete(pagesToDelete);

                // Save the modified document (using the standard Save method)
                doc.Save(outputPdfPath);
            }

            Console.WriteLine($"Pages deleted successfully. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}