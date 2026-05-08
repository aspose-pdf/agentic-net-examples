using System;
using System.IO;
using System.Linq;
using Aspose.Pdf; // Document, PageCollection, etc.

class Program
{
    static void Main()
    {
        // Input PDF and text file containing page numbers to delete (one per line)
        const string inputPdfPath = "input.pdf";
        const string pagesToDeletePath = "pages.txt";
        const string outputPdfPath = "output.pdf";

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

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPdfPath))
            {
                // Read page numbers from the text file, ignore empty/invalid lines
                int[] pagesToDelete = File.ReadAllLines(pagesToDeletePath)
                                          .Select(line => line.Trim())
                                          .Where(line => !string.IsNullOrEmpty(line))
                                          .Select(line =>
                                          {
                                              bool ok = int.TryParse(line, out int num);
                                              return new { ok, num };
                                          })
                                          .Where(x => x.ok && x.num > 0 && x.num <= doc.Pages.Count)
                                          .Select(x => x.num)
                                          .ToArray();

                if (pagesToDelete.Length == 0)
                {
                    Console.WriteLine("No valid page numbers found to delete.");
                }
                else
                {
                    // Delete the specified pages (1‑based indexing)
                    doc.Pages.Delete(pagesToDelete);
                    Console.WriteLine($"Deleted pages: {string.Join(", ", pagesToDelete)}");
                }

                // Save the modified PDF
                doc.Save(outputPdfPath);
                Console.WriteLine($"Result saved to '{outputPdfPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Exception: {ex.Message}");
        }
    }
}