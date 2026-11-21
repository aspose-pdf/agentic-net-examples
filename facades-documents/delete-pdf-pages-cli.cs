using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;

class Program
{
    // Entry point: args[0] = input PDF, args[1] = output PDF, args[2] = pages to delete (e.g., "1,3-5,7")
    static void Main(string[] args)
    {
        if (args.Length < 3)
        {
            Console.WriteLine("Usage: DeletePages <input.pdf> <output.pdf> <pages>");
            Console.WriteLine("Pages format: comma‑separated list, ranges allowed with '-'. Example: 1,3-5,7");
            return;
        }

        string inputPath  = args[0];
        string outputPath = args[1];
        string pagesSpec  = args[2];

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Convert the pages specification into a list of 1‑based page numbers
        List<int> pagesToDelete = ParsePages(pagesSpec);
        if (pagesToDelete.Count == 0)
        {
            Console.Error.WriteLine("No valid pages specified for deletion.");
            return;
        }

        try
        {
            // Load the PDF with Document (PdfFileEditor does not provide DeletePages)
            Document pdfDoc = new Document(inputPath);

            // Delete pages in descending order to keep indexes stable
            pagesToDelete.Sort();
            pagesToDelete.Reverse();
            foreach (int pageNum in pagesToDelete)
            {
                if (pageNum > 0 && pageNum <= pdfDoc.Pages.Count)
                {
                    pdfDoc.Pages.Delete(pageNum);
                }
                else
                {
                    Console.Error.WriteLine($"Page number {pageNum} is out of range (1‑{pdfDoc.Pages.Count}). Skipping.");
                }
            }

            pdfDoc.Save(outputPath);
            Console.WriteLine($"Deleted pages. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during page deletion: {ex.Message}");
        }
    }

    // Parses a string like "1,3-5,7" into a sorted list of distinct page numbers
    static List<int> ParsePages(string spec)
    {
        var result = new List<int>();
        var parts = spec.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

        foreach (var part in parts)
        {
            if (part.Contains("-"))
            {
                var range = part.Split('-');
                if (range.Length == 2 &&
                    int.TryParse(range[0], out int start) &&
                    int.TryParse(range[1], out int end) &&
                    start > 0 && end >= start)
                {
                    for (int i = start; i <= end; i++)
                        result.Add(i);
                }
            }
            else if (int.TryParse(part, out int page) && page > 0)
            {
                result.Add(page);
            }
        }

        // Remove duplicates and sort ascending (Aspose.Pdf expects 1‑based page numbers)
        var distinct = new HashSet<int>(result);
        var sorted = new List<int>(distinct);
        sorted.Sort();
        return sorted;
    }
}
