using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";
        // Output PDF file path after deletion
        const string outputPdf = "output.pdf";
        // Text file containing page numbers to delete (one number per line)
        const string pagesToDeleteFile = "pages_to_delete.txt";

        // Validate input files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(pagesToDeleteFile))
        {
            Console.Error.WriteLine($"Page list file not found: {pagesToDeleteFile}");
            return;
        }

        try
        {
            // Read page numbers from the text file, ignoring empty lines and whitespace
            int[] pagesToDelete = File.ReadAllLines(pagesToDeleteFile)
                                      .Select(line => line.Trim())
                                      .Where(line => !string.IsNullOrEmpty(line))
                                      .Select(line =>
                                      {
                                          // Try to parse each line as an integer
                                          if (int.TryParse(line, out int pageNum))
                                              return pageNum;
                                          else
                                              throw new FormatException($"Invalid page number: '{line}'");
                                      })
                                      .ToArray();

            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdf))
            {
                // Ensure the page numbers are within the valid range (1‑based)
                int maxPage = doc.Pages.Count;
                int[] validPages = pagesToDelete
                                   .Where(p => p >= 1 && p <= maxPage)
                                   .Distinct()
                                   .ToArray();

                if (validPages.Length == 0)
                {
                    Console.WriteLine("No valid page numbers to delete.");
                }
                else
                {
                    // Delete the specified pages in a single call
                    doc.Pages.Delete(validPages);
                }

                // Save the modified document
                doc.Save(outputPdf);
            }

            Console.WriteLine($"Pages deleted and saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}